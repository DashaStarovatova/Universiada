export function Hero() {
  const scrollToRegistration = () => {
    const el = document.getElementById("timeline");
    if (el) el.scrollIntoView({ behavior: "smooth", block: "start" });
  };

  const scrollTo = (id: string) => {
    document.getElementById(id)?.scrollIntoView({ behavior: "smooth" });
  };


  return (
    <section
      id="about"
      className="relative overflow-hidden pt-16 pb-16 md:pt-36 md:pb-24"
    >
      {/* <div className="glow-orb -left-48 -top-56 h-[560px] w-[560px] md:h-[760px] md:w-[760px]" />
      <div className="glow-orb -right-64 -top-44 h-[620px] w-[620px] md:h-[860px] md:w-[860px] [--orb-a:rgba(34,211,238,0.6)] [--orb-b:rgba(168,85,247,0.5)]" /> */}

      <div className="relative mx-auto max-w-[1200px] px-4">
        <div className="mt-6 md:mt-10">
          <div className="inline-flex items-center gap-2 rounded-full border border-white/70 bg-white/70 px-4 py-2 text-[0.75rem] font-semibold text-slate-800 shadow-sm backdrop-blur-md">
            <span className="h-2 w-2 rounded-full bg-[#8232a1]" />
            Экономика • Эконометрика • ДКП
          </div>


          <h1 className="mt-6 text-balance text-[2.4rem] font-bold leading-[1.02] tracking-tight text-slate-950 md:text-[4.2rem]">
            <span className="text-slate-950 uppercase">ЭКОНОМИЧЕСКАЯ УНИВЕРСИАДА</span>{" "}
            <span className=" tracking-normal">2026</span>
          </h1>

          {/* <p className="mt-5 max-w-[56rem] text-[1.05rem] leading-relaxed text-slate-700 md:text-[1.35rem]">
            Прием заявок уже открыт
          </p> */}

          <div className="mt-8 flex flex-wrap items-center gap-4">
            <button
              onClick={() => window.open("https://forms.yandex.ru/u/69d51eb149af472593d0b89d/", "_blank")}
              className="group cursor-pointer inline-flex items-center gap-2 rounded-full bg-slate-950 px-7 py-3 text-[1rem] font-bold text-white shadow-lg shadow-slate-950/20 transition hover:-translate-y-0.5 hover:bg-slate-900 active:translate-y-0"
              >
              Принять участие
              <span className="inline-block h-2 w-2 rounded-full bg-[#8fff8f] shadow-[0_0_18px_rgba(255,106,223,0.8)] transition group-hover:scale-110" />
              </button>
              <button
              onClick={() => scrollTo("benefits")}
              className="inline-flex cursor-pointer items-center rounded-full border border-white/70 bg-white/70 px-6 py-3 text-[1rem] font-bold text-slate-900 shadow-sm backdrop-blur-md transition hover:-translate-y-0.5 hover:bg-white hover:shadow-md active:translate-y-0"
            >
              Подробнее
            </button>
          </div>




          <div className="pb-16 md:pb-16"> {/* отступ внизу */}
            {/* содержимое */}
          </div>




        </div>
      </div>

      {/* Ровная волна-переход к следующей секции */}
      {/* <div className="pointer-events-none absolute inset-x-0 bottom-0">
        <svg
          viewBox="0 0 1440 220"
          className="block h-[160px] w-full md:h-[220px]"
          preserveAspectRatio="none"
          aria-hidden="true"
        >
<path
  d="M0,80 C300,80 400,200 720,200 C1040,200 1140,80 1440,80 V220 H0 Z"
  fill="#ffffff"
/>
        </svg>
      </div> */}
    </section>
  );
}

