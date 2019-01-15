const MonacoWebpackPlugin = require('monaco-editor-webpack-plugin');
const path = require('path');

module.exports = {
    entry: './monaco-main.js',
    output: {
        path: path.resolve(__dirname, '../../wwwroot/dist'),
        filename: 'monaco.js'
    },
    module: {
        rules: [{
            test: /\.css$/,
            use: ['style-loader', 'css-loader']
        }]
    },
    plugins: [
        new MonacoWebpackPlugin()
    ]
};