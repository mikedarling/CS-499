const gulp = require('gulp');
const uglify = require('gulp-uglify');
const rename = require('gulp-rename');
const concat = require('gulp-concat');
const debug = require('gulp-debug');

const appPath = '../src/Web/AnimalRescue.Web/assets/js/';

const concatJs = () => {
    return gulp.src('./angular/**/*.js')
        .pipe(debug())
        .pipe(concat('app.js'))
        .pipe(gulp.dest(appPath));
};

const uglifyJs = () => {
    return gulp.src(appPath + 'app.js')
        .pipe(uglify())
        .pipe(rename({
            suffix: '.min'
        }))
        .pipe(gulp.dest(appPath));
};

const build = gulp.series(concatJs, uglifyJs);

module.exports = {
    default: build,
    build: build
};