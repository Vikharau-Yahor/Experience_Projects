const path = require('path');

module.exports = {
    mode: 'development',
    entry: './src/Test.js',
    output: {
        filename: 'bundle.js',
        path: path.resolve(__dirname, 'public/js'),
    },
    devtool: 'inline-source-map', // enables react debug (i.e. source mapping for react bundled code)
    module: {
        rules: [
          {
            test: /\.m?js$/,
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