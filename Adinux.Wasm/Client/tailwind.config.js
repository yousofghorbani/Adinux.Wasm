/** @type {import('tailwindcss').Config} */
module.exports = {
    important: false,
    safelist: [
        {
            pattern: /bg-(red|blue|green|orange)-(100|800)/,
           // pattern: /.*/,

        }
    ],
    content: ["**/*.razor", "**/*.cshtml", "**/*.html","./node_modules/flowbite/**/*.js"],
    theme: {
        extend: {},
    },
    plugins: [
        require('flowbite/plugin')
    ]
}
