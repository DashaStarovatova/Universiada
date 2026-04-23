import { useRef, useEffect, useState } from "react";
import { timeline } from "../data/mockData";

const downloadPDF = () => {
  const link = document.createElement('a');
  link.href = '../assets/calendar.pdf';
  link.download = 'calendar-ekon-universiada.pdf';
  document.body.appendChild(link);
  link.click();
  document.body.removeChild(link);
};

export function Timeline() {
  const circlesRef = useRef<(HTMLDivElement | null)[]>([]);
  const [lineTop, setLineTop] = useState(76);

  useEffect(() => {
    if (circlesRef.current.length > 0) {
      // Находим позицию первого кружка
      const firstCircle = circlesRef.current[0];
      if (firstCircle) {
        const rect = firstCircle.getBoundingClientRect();
        const containerRect = firstCircle.parentElement?.parentElement?.getBoundingClientRect();
        if (containerRect) {
          // Вычисляем позицию линии относительно родителя
          const top = rect.top + rect.height / 2 - containerRect.top;
          setLineTop(top);
        }
      }
    }
  }, []);

  return (
    <section id="timeline" className="relative pb-16 md:pb-28 pt-10 scroll-mt-20">
      <div className="relative mx-auto max-w-[1200px] px-4">

        {/* Заголовок и кнопка */}
        <div className="flex flex-col gap-4 md:flex-row md:items-end md:justify-between">
          <div className="text-[1.6rem] font-extrabold text-slate-950 md:text-[2.3rem]">
            ЭТАПЫ ПРОВЕДЕНИЯ УНИВЕРСИАДЫ
          </div>

          <a
  href="/calendar.pdf"
  download="Календарь_универсиады.pdf"
  className="inline-flex w-fit items-center gap-2 rounded-full bg-slate-950 px-6 py-3 text-[0.95rem] font-bold text-white shadow-lg shadow-slate-950/20 transition hover:-translate-y-0.5 hover:bg-slate-900 active:translate-y-0"
>
  Скачать календарь
  <span className="h-2 w-2 rounded-full bg-[#8fff8f] shadow-[0_0_18px_rgba(124,246,255,0.8)]" />
</a>
        </div>

        {/* Заголовок и кнопка */}
        <div className="flex flex-col gap-4 md:flex-row md:items-end md:justify-between">
          <div className="text-[0.95rem] font-semibold uppercase tracking-[0.12em] text-slate-900">
            регистрация уже идет
          </div>
        </div>


        {/* Таймлайн с кружками */}
        <div className="relative w-full mt-28">
          {/* Линия на уровне кружков */}
          <div
            className="absolute left-0 right-0 h-[3px] rounded-full bg-white opacity-60"
            style={{ top: `${lineTop}px` }}
          />

          {/* ПРЯМОУГОЛЬНИК от линии до низа текстовых блоков */}
          <div
            className="absolute left-0 right-0 rounded-b-2xl"
            style={{
              top: `${lineTop}px`,
              bottom: '-30px',
              background: 'linear-gradient(to bottom, rgba(255,255,255,0.9) 0%, rgba(255,255,255,0.9) 0%, rgba(255,255,255,0.4) 20%, rgba(255,255,255,0.05) 70%, transparent 100%)',
            }}
          />

          <div className="relative z-10 flex items-start justify-between">
            {timeline.map((t, idx) => (
              <div
                key={t.id}
                className="flex flex-col items-center flex-1 min-w-0 px-2"
              >
                {/* Заголовок сверху */}
                <div className="h-12 mb-2">
                  <span className="text-[1rem] font-bold text-black text-center block uppercase">
                    {(() => {
                      const text = t.label;

                      // Проверка на "в Москве"
                      if (text.includes('в Москве') || text.includes('в москве')) {
                        return (
                          <>
                            Очный финал
                            <br />
                            в Москве
                          </>
                        );
                      }

                      // Проверка на "заявок"
                      if (text.includes('заявок')) {
                        const parts = text.split('заявок');
                        return (
                          <>
                            {parts[0]}заявок
                            {parts[1] && <br />}
                            {parts[1]}
                          </>
                        );
                      }

                      return text;
                    })()}
                  </span>
                </div>

                {/* Кружок */}
                <div
                  ref={(el) => { circlesRef.current[idx] = el; }}
                  className="flex h-16 w-16 items-center justify-center rounded-full bg-white shadow-md z-20"
                >
                  <span className="text-[1.1rem] font-extrabold text-slate-950">
                    {t.date}
                  </span>
                </div>

                {/* Описание снизу */}
                <div className="min-h-[64px] mt-3">
                  <p className="text-[0.95rem] leading-relaxed text-black text-center px-1">
                    {t.description}
                  </p>
                </div>

              </div>
            ))}
          </div>
        </div>

      </div>
    </section>
  );
}