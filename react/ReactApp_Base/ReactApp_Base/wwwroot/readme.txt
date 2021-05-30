﻿**********
Setup Base: Webpack, Babel, React
**********
- create 'public/js' and 'src' folder
- run 'npm install webpack' in wwroot ----> packages.json, packages-lock.json node_modules are created
- create 'webpack.config.js' in root folder
- run 'npm install -D babel-loader @babel/core @babel/preset-env @babel/preset-react' -- isntalls babel-loader and its dependencies
- run 'npm install react react-dom' installs react modules
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
- install React DevTools Extension for your browser

******************
Enable TypeScript (webpack, babel)
**********************************
1) run 'npm install @babel/preset-typescript' in wwroot
2) update webpack.config by adding 'resolve' node and updating babel module:
    resolve: {
        extensions: ['.tsx', '.ts', '.js'],
    },
    module: {
        rules: [
            {
                test: /\.tsx$/,
                exclude: /(node_modules|bower_components)/,
                use: {
                    loader: 'babel-loader',
                    options: {
                        presets: ['@babel/preset-env', "@babel/preset-react", "@babel/preset-typescript"]
                    }
                }
            }
        ]
    }
3) run 'npm run build' to build bundle
4) optionally, add "tsconfig.json" which is used by Visual studio (e.g. for Intellisence in TS code):
    {
  "compilerOptions": {
    "noImplicitAny": true,
    "allowSyntheticDefaultImports": true,
    "forceConsistentCasingInFileNames": true,
    "module": "es6",
    "target": "es5",
    "jsx": "react",
    "allowJs": true,
    "moduleResolution": "node"
  }
}

*****************
Setup Redux
***************
- npm install redux
- npm install react-redux
- Install Redux DevTools for your browser


************
WebPack Live rebuild and page reload
******************
- https://dev.to/jayotterbein/how-to-get-hot-module-reload-with-custom-webpack-in-asp-net-core-3-1-5a7m
- npm install webpack-dev-server --save-dev
- nuget package must be installed: "Microsoft.AspNetCore.SpaServices.Extensions"
- add to webpck.config.json
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
- add script to package.json:
   "serve": "webpack serve"
- update Startup configure
            if (env.IsDevelopment())
            {
                app.Map(
                    "/js",
                    ctx => ctx.UseSpa(
                        spa =>
                        {
                            spa.Options.SourcePath = "wwwroot/public";
                            spa.UseProxyToSpaDevelopmentServer("http://localhost:3001/");
                        })); ;
            }
            else
            {
                app.UseStaticFiles();
            }
- run command "npm run serve" + run asp.net server
Note: asp.net server will get static files from webpack server which will rebuild any affected scripts + reload browser