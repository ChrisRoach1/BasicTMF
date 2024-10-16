/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}",
  ],
  theme: {
    extend: {},
  },
  plugins: [require("daisyui")],
  daisyui: {
    themes: ['light'],
    styled: true,
    base: true,
    utils: true,
    logs: true,
    rtl: false,
  },
}
