const webpack = require("webpack"),
    { merge } = require("webpack-merge"),
    common = require("./webpack.config"),
    devCerts = require("office-addin-dev-certs"),
    CopyPlugin = require("copy-webpack-plugin"),
    p = (p) => path.join(__dirname, "../", p || ""),
    path = require("path");

process.env.TAILWIND_MODE = "watch";

module.exports = async () => {
    // let ssl = await devCerts.getHttpsServerOptions();

    return merge(common({ tailwindOptions: {}, rootCssLoader: "style-loader" }), {
        mode: "development",

        //plugins: [new webpack.HotModuleReplacementPlugin()],

        devtool: "eval",

        output: {
            publicPath: "/",
        },

        plugins: [
            new webpack.HotModuleReplacementPlugin(),
            new webpack.DefinePlugin({
                API_BASE_URL: JSON.stringify("/api"),
                "process.env.NODE_ENV": JSON.stringify("development"),
                "process.env.REACT_APP_BASE_API_URL": JSON.stringify("http://127.0.0.1:5288"),
            }),
            new CopyPlugin({
                patterns: [{ from: p("./assets"), to: p("./dist/assets") }],
            }),
        ],

        devServer: {
            hot: true,
            port: 5544,
            historyApiFallback: true,
            // https: {
            //     ca: ssl.ca,
            //     key: ssl.key,
            //     cert: ssl.cert,
            //     passphrase: "webpack-dev-server",
            // },
            proxy: {
                "/api": {
                    target: "http://localhost:5288",
                    // router: () => "https://localhost:7297",
                    secure: false,
                },
                "/hubs/devices": "http://localhost:5288",
            },
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Methods": "GET, POST, PUT, DELETE, PATCH",
                "Access-Control-Allow-Headers": "X-Requested-With, content-type, Authorization",
            },
        },
    });
};
