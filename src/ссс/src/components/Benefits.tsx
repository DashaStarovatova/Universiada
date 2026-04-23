import { useState } from "react";
import { benefits } from "../data/mockData";
import { GradientArrows } from "./GradientArrows";

export function Benefits() {
  const [activeIndex, setActiveIndex] = useState(0);

  const nextSlide = () => {
    setActiveIndex((prev) => (prev + 1) % benefits.length);
  };

  const prevSlide = () => {
    setActiveIndex((prev) => (prev - 1 + benefits.length) % benefits.length);
  };

  return (
    <section id="benefits" className="relative scroll-mt-12 overflow-hidden pt-16 pb-16 md:pt-16 md:pb-16">
      {/* Белый прямоугольник с отступами сверху и снизу
    <div className="absolute inset-0 pointer-events-none -z-10">
      <div className="w-full max-w-[1180px] mx-auto h-[calc(100%-80px)] top-10 bottom-10 rounded-xl bg-white/95 shadow-lg shadow-slate-950/50" />
    </div> */}
      <div className="relative mx-auto max-w-[1200px] px-4">
        <div className="w-full max-w-[1180px] mx-auto rounded-xl bg-white/95 shadow-lg shadow-slate-950/50 overflow-hidden">
          <div className="px-4 py-8 md:px-6 md:py-10">

            {/* Левая часть (текст) и правая часть (клякса) в одной строке */}
            <div className="flex flex-col gap-8 lg:flex-row lg:items-center lg:gap-6">
              {/* ЛЕВАЯ КОЛОНКА — текст */}
              <div className="flex-1 lg:max-w-[50%] lg:ml-14">
                {/* Теперь "Экономическая универсиада — это" прямо над основным текстом */}
                <div className="mb-3 flex items-center gap-3">
                  <GradientArrows />
                  <span className="text-[0.95rem] font-semibold uppercase tracking-[0.12em] text-slate-900">
                    Экономическая универсиада — это
                  </span>
                </div>

                <p className="text-[1.35rem] font-semibold leading-snug text-slate-900 md:text-[2.2rem]">
                  ВСЕРОССИЙСКАЯ ОЛИМПИАДА
                  <br />
                  ДЛЯ СТУДЕНТОВ ПО{" "}
                  <span className="relative inline-block">
                    <span className="absolute inset-x-0 bottom-2 h-[30%] bg-gradient-to-r from-[#783baa] via-[#9e9fdb] to-[#8fff8f] rounded-full opacity-70 blur-[1px]" />
                    <span className="relative z-10">ДЕНЕЖНО-КРЕДИТНОЙ</span>
                  </span>
                  <br />
                  ПОЛИТИКЕ
                </p>

                <div className="mb-3 flex items-center gap-3 mt-16">
                  <span className="text-[0.5rem] font-semibold uppercase tracking-[0.12em] text-slate-900">
                    центральный банк российской федерации | МГУ им. ломоносова
                  </span>
                </div>

              </div>



              {/* Правая часть — фиолетовая клякса со слайдером */}
              <div className="relative flex-1 flex justify-start lg:justify-center lg:-ml-8">
                <div className="relative w-100 h-96 md:w-96 md:h-[420px]">

                  {/* Контурная клякса (черная граница) */}
                  <div
                    className="absolute inset-0"
                    style={{
                      borderRadius: "35% 65% 70% 30% / 45% 40% 60% 55%",
                      transform: "rotate(-30deg)",
                      padding: "3px",
                      background: "linear-gradient(135deg, #ff6adf, #6a8bff, #7cf6ff)",
                      WebkitMask: "linear-gradient(#fff 0 0) content-box, linear-gradient(#fff 0 0)",
                      WebkitMaskComposite: "xor",
                      maskComposite: "exclude"
                    }}
                  />

                  <div
                    className="absolute inset-0 bg-gradient-to-br from-[#93d6b1] via-[#93d6b1] to-[#93d6b1]"
                    style={{
                      borderRadius: "40% 60% 70% 30% / 40% 50% 50% 60%",
                      transform: "rotate(45deg)"
                    }}
                  />

                  {/* Фиолетовая клякса*/}
                  <div
                    className="absolute inset-0 md:w-100 md:h-100 bg-gradient-to-br from-[#783baa] via-[#783baa] to-[#783baa]"
                    style={{
                      borderRadius: "40% 60% 70% 30% / 40% 50% 50% 60%",
                    }}
                  />

                  {/* Содержимое внутри кляксы */}
                  <div className="relative z-10 flex flex-col h-full p-6 md:p-10 md:pl-6 text-center">

                    {/* Верхний текст — смещаем вниз (ближе к центру) */}
                    <div className="mt-12 md:mt-16">
                      <h2 className="text-xs font-semibold uppercase tracking-wide text-white/80 md:text-xs text-center">
                        ПОПРОБУЙ СЕБЯ В РОЛИ
                      </h2>
                      <h3 className="mt-1 text-xl font-bold text-white md:text-xl text-center">
                        МЕГАРЕГУЛЯТОРА
                      </h3>
                    </div>

                    {/* Основной текст — оставляем как было */}
                    <div className="mt-8 flex-1 w-full">
                      <p className="text-2xl leading-relaxed uppercase text-white md:text-2xl text-center">
                        {benefits[activeIndex].title}
                      </p>
                    </div>

                    {/* Навигация — поднимаем выше */}
                    <div className="flex items-center justify-center gap-4 mb-16">
                      <div className="flex gap-2">
                        {benefits.map((_, idx) => (
                          <button
                            key={idx}
                            onClick={() => setActiveIndex(idx)}
                            className={`cursor-pointer h-2 rounded-full transition-all ${activeIndex === idx
                              ? "w-6 bg-white"
                              : "w-2 bg-white/40 hover:bg-white/60"
                              }`}
                          />
                        ))}
                      </div>

                      <button
                        onClick={nextSlide}
                        className="cursor-pointer flex h-8 w-8 items-center justify-center rounded-full bg-white/20 text-white transition hover:bg-white/30"
                      >
                        <svg className="h-4 w-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                          <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M9 5l7 7-7 7" />
                        </svg>
                      </button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
}