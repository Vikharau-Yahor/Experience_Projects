**********
Setup Base: Webpack, Babel, React
**********
- create 'public/js' and 'src' folder
- run 'npm install webpack' in wwroot ----> packages.json, packages-lock.json node_modules are created
- create 'webpack.config.js' in root folder
- run 'npm install -D babel-loader @babel/core @babel/preset-env @babel/preset-react' -- isntalls babel-loader and its dependencies
- run 'npm isntall react react-dom' installs react modules
- add to 'webpack.config.js':
        const path = require('path');

        module.exports = {
        mode: 'development',
        entry: './src/Test.jsx',
        output: {
            filename: 'bundle.js',
            path: path.resolve(__dirname, 'public/js'),
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
- add script to packages.json
      "scripts": {
        "build": "webpack"
      },
- add 'src/Test.jsx':
        import React from 'react';
        import ReactDOM from 'react-dom'

        class CommentBox extends React.Component {
            render() {
                return (
                    <div className="commentBox">Hello, world from React! I am a CommentBox.</div>
                );
            }
        }

        ReactDOM.render(<CommentBox />, document.getElementById('content'));
- add 'public/index.html':
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset="utf-8" />
            <title>Home</title>
        </head>
        <body>
            <h1>Home page</h1>
            <div id="content">

            </div>
            <script src="/js/bundle.js"></script>
        </body>
        </html>
- Install React DevTools Extension for your browser

*****************
Setup Redux
***************
- npm install redux
- npm install react-redux
- Install Redux DevTools for your browser