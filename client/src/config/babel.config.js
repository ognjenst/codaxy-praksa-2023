module.exports = {
    cacheDirectory: true,
    cacheIdentifier: "v9",
    presets: [
        [
            "@babel/preset-typescript",
            {
                targets: {
                    chrome: 60,
                },
                // corejs: 3,
                // useBuiltIns: 'usage',
                modules: false,
                loose: true,

                cx: {
                    jsx: {
                        trimWhitespace: true,
                    },
                    imports: {
                        useSrc: true,
                    },
                },
            },
        ],
        ["@babel/preset-react", { runtime: "automatic" }],
    ],
    plugins: [
        ["@babel/plugin-proposal-private-methods", { loose: false }],
        ["@babel/plugin-proposal-private-property-in-object", { loose: false }],
    ],
};
