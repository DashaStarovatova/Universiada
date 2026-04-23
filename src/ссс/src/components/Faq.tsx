import { useState } from "react";
import { faqs } from "../data/mockData";

export function Faq() {
  const [openId, setOpenId] = useState<number | null>(null);

  return (
    <section id="faq" className="relative pb-16 md:pb-28 scroll-mt-40">
      <div className="relative mx-auto max-w-[1200px] px-4">
        <div className="flex flex-col gap-4 md:flex-row md:items-end md:justify-between">
          <div>
            <div className="mt-2 text-[1.6rem] uppercase font-extrabold text-slate-950 md:text-[2.3rem]">
              Часто задаваемые вопросы
            </div>
          </div>
        </div>

        {/* Список вопросов без белых прямоугольников */}
        <div className="mt-8">
          {/* Линия перед первым вопросом */}
          <div className="h-px w-full bg-slate-600" />

          {faqs.map((item, index) => {
            const isOpen = openId === item.id;
            return (
              <div key={item.id}>
                {/* Вопрос */}
                <button
                  className="cursor-pointer flex w-full items-center justify-between gap-4 py-5 text-left text-[1.3rem] font-bold text-black"
                  onClick={() => setOpenId(isOpen ? null : item.id)}
                  aria-expanded={isOpen}
                >
                  <span>{item.question}</span>
                  <span className="relative flex h-8 w-8 items-center justify-center rounded-full bg-slate-950 text-white shadow-sm">
                    <span className="absolute h-[2px] w-4 rounded-full bg-current" />
                    <span
                      className={`absolute h-[2px] w-4 rounded-full bg-current transition ${isOpen ? "rotate-0 opacity-0" : "rotate-90 opacity-100"
                        }`}
                    />
                  </span>
                </button>

                {/* Ответ с прозрачным белым фоном */}
                <div
                  className={`grid transition-all duration-300 ease-out ${isOpen ? "grid-rows-[1fr]" : "grid-rows-[0fr]"
                    }`}
                >
                  <div
                    className={`overflow-hidden text-[0.98rem] leading-relaxed transition-[padding-bottom,opacity] duration-300 ${isOpen ? "pb-5 opacity-100" : "pb-0 opacity-0"
                      }`}
                  >
                    {/* <div className="bg-white/40 rounded-xl p-4 text-black">
                      {item.answer}
                    </div> */}


                    <p
                      className="bg-white/60 rounded-xl p-4 text-black"
                      dangerouslySetInnerHTML={{ __html: item.answer }}
                    />

                  </div>
                </div>

                {/* Горизонтальная линия-разделитель (после каждого вопроса, кроме последнего) */}
                {index < faqs.length - 1 && (
                  <div className="h-px w-full bg-slate-600" />
                )}
              </div>
            );
          })}

          {/* Линия после последнего вопроса */}
          <div className="h-px w-full bg-slate-600" />
        </div>
      </div>
    </section>
  );
}