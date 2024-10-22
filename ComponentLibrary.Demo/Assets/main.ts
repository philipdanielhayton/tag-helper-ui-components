import "./app.postcss";
import "htmx.org/dist/htmx";
import Alpine from "alpinejs";
import focus from "@alpinejs/focus";

import { createCarousel } from "$lib/components";

window.Alpine = Alpine;

// Alpine plugins go here
Alpine.plugin(focus);

//  Stores go here


//  Components
Alpine.data("carousel", (opts) => createCarousel(opts));
Alpine.start();
