import { defineConfig } from "vite";
import { setDefaultResultOrder } from `dns`
import mkcert from "vite-plugin-mkcert";
import copy from "rollup-plugin-copy";
import path from "path";

setDefaultResultOrder("verbatim");

export default defineConfig({
  build: {
    manifest: true,
    rollupOptions: {
      input: {
        main: "./assets/main.ts",
        //sentry: "./src/sentry.ts",
      },
      output: {
        entryFileNames: `[name].[hash].js`,
        chunkFileNames: `[name].[hash].js`,
        assetFileNames: `[name].[hash].[ext]`,
      },
    },
    outDir: "./wwwroot/assets",
    assetsInlineLimit: 0,
  },
  server: {
    https: true,
    strictPort: true,
    port: 5173,
    hmr: {
      port: 5173
    }
  },
  resolve: {
    alias: {
      $lib: path.resolve("./assets/lib"),
    },
  },
  plugins: [
    mkcert(),
    copy({
      targets: [
        { src: "./src/images/**/*", dest: "./wwwroot/assets/images/" },
        { src: "./src/webfonts/**/*", dest: "./wwwroot/assets/webfonts/" },
      ],
      verbose: true,
      copyOnce: true,
      hook: "writeBundle",
    }),
    copy({
      targets: [
        { src: "./wwwroot/assets/main.*.css", dest: "./wwwroot/backend", rename: 'main.css' },
      ],
      verbose: true,
      copyOnce: true,
      hook: "closeBundle",
    }),
  ],
});
