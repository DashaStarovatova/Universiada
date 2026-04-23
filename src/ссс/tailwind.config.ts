import type { Config } from "tailwindcss";

export default {
  content: ["./index.html", "./src/**/*.{ts,tsx,js,jsx}"],
  theme: {
    extend: {
      fontFamily: {
        // sans: ["system-ui", "sans-serif"],
        sans: ["Space Grotesk", "system-ui", "sans-serif"],
      },
      colors: {
        primary: "#FF6ADF",
        secondary: "#6A8BFF",
        accent: "#7CF6FF",
      },
      boxShadow: {
        card: "0 18px 40px rgba(30, 64, 175, 0.18)",
      },
    },
  },
  plugins: [],
} satisfies Config;

