/*
This file is the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. https://go.microsoft.com/fwlink/?LinkId=518007
*/

const gulp = require("gulp"),
    uglify = require("gulp-uglify"),
    concat = require("gulp-concat"),
    rimraf = require("rimraf"),
    merge = require("merge-stream");

// *** Minify the sites custom javascript ***
// *** Move it to the lib/site directory ***
gulp.task("minify", function () {
    let streams = [
        gulp.src(["wwwroot/js/*.js"])
            .pipe(uglify())
            .pipe(concat("site.min.js"))
            .pipe(gulp.dest("wwwroot/lib/site"))
    ];

    return merge(streams);
});

// *** Dependency directories ***
const dependencies = {
    "bootstrap": {
        "dist/**/*": ""
    },
    "chosen-js": {
        "dist/*": ""
    },
    "@fortawesome/fontawesome-free": {
        "dist/**/*": ""
    },
    "jquery": {
        "dist/*": ""
    },
    "jquery-validation": {
        "dist/**/*": ""
    },
    "jquery-validation-unobtrusive": {
        "dist/*": ""
    },
    "popper.js": {
        "dist/**/*": ""
    }
};

// *** Clean the 3rd party code directory ***
gulp.task("clean", function (cb) {
    return rimraf("wwwroot/vendor/", cb);
});

// *** Build the script dependencies ***
gulp.task("scripts", function () {
    let streams = [];
    for (let dependency in dependencies) {
        console.log(`Processing scripts for: ${dependency}`);
        for (let directory in dependencies[dependency]) {
            streams.push(gulp.src(`node_modules/${dependency}/${directory}`)
                .pipe(gulp.dest(`wwwroot/vendor/${dependencies[dependency][directory]}`)));
        }
    }
    return merge(streams);
});

gulp.task("default", ['clean', 'minify', 'scripts']);