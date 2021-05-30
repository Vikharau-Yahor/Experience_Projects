const path = require('path');

module.exports = {
    mode: 'development',
    entry: './src/Test.jsx',
    output: {
        filename: 'bundle.js',
        path: path.resolve(__dirname, 'public/js'),
    },
    devServer: {
        contentBase: path.join(__dirname, 'public'),
        compress: true,
        // this enabled hot module replacement of modules so when you make a change in a javascript or css file the change will reflect on the browser
        hot: true,
        // this uses websockets for communication for hot module reload, and websockets are planned to be the default for the 5.x release
        transportMode: 'ws',
        // port that the webpack-dev-server runs on; must match the later configuration where ASP.NET Core knows where to execute
        port: 3001
    },
    devtool: 'inline-source-map', // enables react debug (i.e. source mapping for react bundled code)
    module: {
        rules: [
            {
                test: /\.m?jsx$/,
                exclude: /(node_modules|bower_components)/,
                use: {
                    loader: 'babel-loader',
                    options: {
                        presets: ['@babel/preset-env', "@babel/preset-react"]
                    }
                }
            }
        ]
    }
}