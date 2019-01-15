const path = require('path');
console.log(__dirname);
module.exports = {
    entry: './tests-main.js',
    mode: 'development',
    output: {
        filename: "tests-main.js",
        path: path.resolve(__dirname, '../../wwwroot/dist')
    }
}