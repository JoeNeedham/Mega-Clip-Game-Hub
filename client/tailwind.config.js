/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{html,ts}"],
  theme: {
    extend: {
      colors: {
        transparent: "transparent",
        primary: {
          DEFAULT: "#638CF6",
          100: "#dce4fd",
          300: "#96b4fa",
          500: "#638CF6",
          700: "#2232d3",
        },
      },
    },
  },
  plugins: [],
};
