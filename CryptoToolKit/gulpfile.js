/// <binding BeforeBuild='build' />
/*
This file is the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. https://go.microsoft.com/fwlink/?LinkId=518007
*/

const gulp = require("gulp"),
    uglify = require("gulp-uglify"),
    concat = require("gulp-concat"),
    rimraf = require("rimraf"),
    merge = require("merge-stream");

// *** Dependency directories ***
const dependencies = {
    "bootstrap": {
        "dist/**/*": "bootstrap/dist"
    },
    "chosen-js": {
        "*": "chosen-js"
    },
    "@fortawesome/fontawesome-free": {
        "css/all.min.css": "fontawesome-free/css",
        "js/all.min.js": "fontawesome-free/js",
        "webfonts/*": "fontawesome-free/webfonts"
    },
    "jquery": {
        "dist/*": "jquery/dist"
    },
    "jquery-slimscroll": {
        "jquery.slimscroll.min.js": "jquery-slimscroll"
    },
    "jquery-validation": {
        "dist/**/*": "jquery-validation/dist"
    },
    "jquery-validation-unobtrusive": {
        "dist/*": "jquery-validation-unobtrusive/dist"
    },
    "moment": {
        "locale/*": "moment/locale",
        "min/*": "moment/min"
    },
    "popper.js": {
        "dist/**/*": "popper.js/dist"
    }
};

// *** Clean the 3rd party code directory ***
function clean(cb) {
    return rimraf("wwwroot/vendor/", cb);
}

// *** Minify the sites custom javascript ***
// *** Move it to the lib/site directory ***
function minify() {
    const streams = [
        gulp.src(["wwwroot/js/*.js"])
        .pipe(uglify())
        .pipe(concat("site.min.js"))
        .pipe(gulp.dest("wwwroot/lib/site"))
    ];

    return merge(streams);
}

//// *** Build the 3rd party / npm dependencies ***
function scripts() {
    const streams = [];
    for (let dependency in dependencies) {
        console.log(`Processing scripts for: ${dependency}`);
        for (let directory in dependencies[dependency]) {
            streams.push(gulp.src(`node_modules/${dependency}/${directory}`)
                .pipe(gulp.dest(`wwwroot/vendor/${dependencies[dependency][directory]}`)));
        }
    }
    return merge(streams);
}

const build = gulp.series(clean, gulp.parallel(minify, scripts));

exports.clean = clean;
exports.minify = minify;
exports.scripts = scripts;
exports.build = build;
