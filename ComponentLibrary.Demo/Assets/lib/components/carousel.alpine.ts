import EmblaCarousel, {
  EmblaCarouselType,
  EmblaOptionsType,
} from "embla-carousel";
import AutoHeight from "embla-carousel-auto-height";
import Autoplay from "embla-carousel-autoplay";
import ClassNames from "embla-carousel-class-names";
import Fade from "embla-carousel-fade";

export type CarouselOptions = {
  options?: EmblaOptionsType;
  autoPlay?: any;
  classNames?: any;
  fade?: any;
  autoHeight?: any;
};

export function createCarousel(opts?: CarouselOptions) {
  return {
    carousel: <EmblaCarouselType | null>null,
    lastUpdatedTimeStamp: null,
    playing: false,
    slidesInView: [],
    getPlayPlugin() {
      const plugins = this.carousel.plugins();
      const plugin = plugins["autoplay"] || plugins["autoscroll"];
      if (!plugin) {
        return false;
      }

      return plugin;
    },
    isInView(index: number) {
      return this.slidesInView.includes(index);
    },
    updatePlayState() {
      let plugin = this.getPlayPlugin();

      if (!plugin) {
        this.playing = false;
        return;
      }

      if (typeof plugin.isPlaying === "undefined") {
        this.playing = false;
        return;
      }

      this.playing = plugin.isPlaying();
    },
    play() {
      let plugin = this.getPlayPlugin();
      if (!plugin || typeof plugin.play === "undefined") {
        console.warn("Autoplay or Autoscroll is not enabled");
        return;
      }
      plugin.play();
      this.updatePlayState();
    },
    stop() {
      let plugin = this.getPlayPlugin();
      if (!plugin || typeof plugin.stop === "undefined") {
        console.warn("Autoplay or Autoscroll is not enabled");
        return;
      }
      plugin.stop();
      this.updatePlayState();
    },
    togglePlay() {
      if (this.playing) {
        this.stop();
      } else {
        this.play();
      }
    },
    init() {
      if (!this.$refs.container) {
        console.error("No container provided for carousel");
        return;
      }

      const plugins = [];
      if (opts?.autoPlay?.active) plugins.push(Autoplay(opts.autoPlay));
      if (opts?.classNames?.active) plugins.push(ClassNames(opts.classNames));
      if (opts?.fade?.active) plugins.push(Fade());
      if (opts?.autoHeight?.active) plugins.push(AutoHeight());

      const emblaOptions = {
        container: this.$refs.container,
        ...opts?.options,
      };

      const api = EmblaCarousel(this.$el, emblaOptions, plugins);

      api.on("init", (carousel) => {
        this.updatePlayState();
        this.$dispatch("carousel:init", carousel);
      });

      api.on("reInit", (carousel) => {
        this.$dispatch("carousel:reInit", carousel);
      });

      api.on("destroy", (carousel) => {
        this.$dispatch("carousel:destroy", carousel);
      });

      api.on("select", (carousel) => {
        this.$dispatch("carousel:select", carousel);
      });

      api.on("scroll", (carousel) => {
        this.$dispatch("carousel:scroll", carousel);
      });

      api.on("settle", (carousel) => {
        this.$dispatch("carousel:settle", carousel);
      });

      api.on("resize", (carousel) => {
        this.$dispatch("carousel:resize", carousel);
      });

      api.on("slidesInView", (carousel) => {
        this.slidesInView = carousel.slidesInView();
        this.$dispatch("slidesInView", carousel);
      });

      api.on("slidesChanged", (carousel) => {
        this.$dispatch("carousel:slidesChanged", carousel);
      });

      api.on("slideFocus", (carousel) => {
        this.$dispatch("carousel:slideFocus", carousel);
      });

      api.on("pointerDown", (carousel) => {
        this.$dispatch("carousel:pointerDown", carousel);
      });

      api.on("pointerUp", (carousel) => {
        this.$dispatch("carousel:pointerUp", carousel);
      });

      api.on("autoplay:play", (evt) => {
        this.$dispatch("carousel:autoplay:play", evt);
      });

      api.on("autoplay:stop", (evt) => {
        this.$dispatch("carousel:autoplay:stop", evt);
      });

      this.carousel = api;
    },
  };
}
