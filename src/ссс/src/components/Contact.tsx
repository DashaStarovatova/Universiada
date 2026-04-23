import bankRussiaLogo from "../assets/bank-russia.png";
import maxLogo from "../assets/Max_logo.svg";
import mguLogo from "../assets/MSU_Logotype.png";

const socialLinks = [
  { id: "tg", label: "Telegram", href: "https://t.me/" },
  { id: "max", label: "MAX", href: "https://max.ru/" },
  { id: "mail", label: "Email", href: "mailto:info@example.com" },
];

function SocialIcon({ id }: { id: string }) {
  if (id === "tg") {
    return (
      <svg viewBox="0 0 24 24" className="h-8 w-8" aria-hidden="true">
        <path
          fill="currentColor"
          d="M19.75 4.2c.4-.16.82.2.72.62l-3.3 15.6c-.08.38-.5.58-.85.4l-5.2-2.8-2.6 2.5c-.28.27-.76.08-.76-.3v-3.7l9.8-8.86c.23-.2-.05-.55-.32-.38l-12.1 7.4-5.1-1.7c-.4-.13-.43-.7-.06-.88L19.75 4.2Z"
        />
      </svg>
    );
  }
  if (id === "mail") {
    return (
      <svg viewBox="0 0 24 24" className="h-9 w-9" aria-hidden="true">
        <path
          fill="currentColor"
          d="M20 6H4a2 2 0 0 0-2 2v8a2 2 0 0 0 2 2h16a2 2 0 0 0 2-2V8a2 2 0 0 0-2-2Zm0 3.2-8 4.8-8-4.8V8l8 4.8L20 8v1.2Z"
        />
      </svg>
    );
  }
  if (id === "max") {
    return (
      <img
        src={maxLogo}
        alt="MAX"
        className="h-8 w-8" // Размер под ваши иконки
      />
    );
  }
}


export function Contact() {
  return (
    <section id="contact" className="relative">

      <div className="relative mx-auto max-w-[1200px] px-4">
        <div className="mt-3 grid gap-6 md:grid-cols-2 md:items-end">
          <div className="flex flex-col gap-4">
            <div className="text-[1.8rem] font-extrabold leading-tight text-slate-950 md:text-[2.5rem]">
              Остались вопросы? 
              <br />
              Напиши нам
            </div>

            <div className="flex items-center gap-2.5">
              {socialLinks.map((l) => (
                <a
                  key={l.id}
                  href={l.href}
                  target={l.href.startsWith("http") ? "_blank" : undefined}
                  rel={l.href.startsWith("http") ? "noreferrer" : undefined}
                  className="inline-flex h-14 w-14 items-center justify-center rounded-full bg-slate-950 text-white shadow-lg shadow-slate-950/20 transition hover:-translate-y-0.5 hover:bg-slate-900 active:translate-y-0"
                  aria-label={l.label}
                >
                  <SocialIcon id={l.id} />
                </a>
              ))}
            </div>
          </div>

          <div className="relative">
            {/* <div className="text-[0.95rem] font-semibold uppercase tracking-[0.14em] text-slate-900 md:text-[0.8rem] max-w-[200px]"> */}
            {/* Проект Северо – Западного
              <br />
              главного управления центрального банка российской федерации
            </div> */}
            <div className="mt-4 flex items-center justify-end gap-6">
              {/* Логотип Банка России */}
              <img
                src={bankRussiaLogo}
                alt="Банк России"
                className="h-14 w-auto opacity-90"
                loading="lazy"
              />

              {/* Вертикальная линия */}
              <div className="h-8 w-px bg-slate-300" />

              {/* Логотип МГУ */}
              <img
                src={mguLogo}
                alt="МГУ имени М.В. Ломоносова"
                className="h-20 w-auto opacity-90"
                loading="lazy"
              />
            </div>
          </div>
        </div>
      </div>
    </section >
  );
}

