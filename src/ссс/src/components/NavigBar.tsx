import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
// npm install react-router-dom

const scrollTo = (id: string) => {
  document.getElementById(id)?.scrollIntoView({ behavior: "smooth" });
};


export function NavigBar() {
  const [scrolled, setScrolled] = useState(false);

  useEffect(() => {
    const handleScroll = () => {
      setScrolled(window.scrollY > 10);
    };
    window.addEventListener("scroll", handleScroll);
    return () => window.removeEventListener("scroll", handleScroll);
  }, []);

  return (
    <nav className={`fixed top-0 left-0 right-0 z-40 transition-all duration-300 ${scrolled
      ? "bg-white/95 py-4"
      : "bg-transparent py-2"
      }`}>
      <div className="mx-auto max-w-[1200px] px-4">
        <div className={`flex items-center justify-center gap-6 transition-all duration-300 ${scrolled ? "pt-2 pb-2" : "pt-6 pb-6"
          }`}>
          <button
            onClick={() => scrollTo("benefits")}
            className="cursor-pointer gradient-border_navig px-5 py-2 text-[0.85rem] font-semibold tracking-wide text-black transition hover:opacity-80 active:translate-y-0"
          >
            ОБ ОЛИМПИАДЕ
          </button>

          <button
            onClick={() => scrollTo("timeline")}
            className="cursor-pointer gradient-border_navig px-5 py-2 text-[0.85rem] font-semibold tracking-wide text-black transition hover:opacity-80 active:translate-y-0"
          >
            ЭТАПЫ
          </button>
          <button
            onClick={() => scrollTo("faq")}
            className="cursor-pointer gradient-border_navig px-5 py-2 text-[0.85rem] font-semibold tracking-wide text-black transition hover:opacity-80 active:translate-y-0"
          >
            ВОПРОСЫ И ОТВЕТЫ
          </button>
          <button
            onClick={() => scrollTo("contact")}
            className="cursor-pointer gradient-border_navig px-5 py-2 text-[0.85rem] font-semibold tracking-wide text-black transition hover:opacity-80 active:translate-y-0"
          >
            КОНТАКТЫ
          </button>

          {/* <button className="gradient-border ml-auto px-5 py-2 text-[0.85rem] font-semibold tracking-wide text-slate-900 shadow-sm shadow-[#ff6adf]/15 transition hover:shadow-md active:translate-y-0"> */}
          <Link
  to="/cabinet"
  className="gradient-border_navig ml-auto px-5 py-2 text-[0.85rem] font-semibold tracking-wide text-slate-900 transition hover:shadow-md active:translate-y-0"
>
  ВОЙТИ
</Link>
        </div>
      </div>
    </nav>
  );
}