import { useState } from "react";

const navItems = [
  { id: "about", label: "Об универсиаде" },
  { id: "benefits", label: "Попробуй себя" },
  { id: "timeline", label: "Этапы" },
  { id: "faq", label: "FAQ" },
  { id: "contact", label: "Контакты" },
];

export function Header() {
  const [open, setOpen] = useState(false);

  const handleNavClick = (id: string) => {
    const element = document.getElementById(id);
    if (element) {
      element.scrollIntoView({ behavior: "smooth", block: "start" });
    }
    setOpen(false);
  };

  return (
    <header className="absolute inset-x-0 top-0 z-40">
      <div className="mx-auto max-w-[1200px] px-4 pt-10">
        <div className="flex items-start justify-end gap-2">
          <button className="relative z-[10000] hidden rounded-md border-2 border-[#ff6adf] bg-white px-5 py-2 text-[0.85rem] font-semibold tracking-wide text-slate-900 shadow-sm shadow-[#ff6adf]/15 transition hover:bg-[#ff6adf]/10 hover:shadow-md active:translate-y-0 md:inline-flex">
            ВОЙТИ
          </button>
          <button
            aria-label="Открыть меню"
            className="inline-flex h-10 w-10 items-center justify-center rounded-md border-2 border-[#ff6adf] bg-white shadow-sm shadow-[#ff6adf]/15 transition hover:bg-[#ff6adf]/10 hover:shadow-md md:hidden"
            onClick={() => setOpen(true)}
          >
            <span className="sr-only">Открыть меню</span>
            <span className="flex w-4 flex-col gap-1">
              <span className="h-[2px] w-full rounded-full bg-slate-900" />
              <span className="h-[2px] w-full rounded-full bg-slate-900" />
              <span className="h-[2px] w-full rounded-full bg-slate-900" />
            </span>
          </button>
        </div>
      </div>

      {open && (
        <div className="fixed inset-0 z-50 bg-slate-950/40 backdrop-blur-sm md:hidden">
          <div className="ml-auto flex h-full w-72 max-w-[80%] flex-col bg-white/95 px-5 pb-6 pt-4 shadow-2xl">
            <div className="mb-6 flex items-center justify-between">
              <span className="text-xs font-semibold uppercase tracking-[0.22em] text-slate-500">
                Меню
              </span>
              <button
                className="flex h-8 w-8 items-center justify-center rounded-full border border-slate-200 bg-white shadow-sm transition hover:border-primary/60 hover:shadow-md"
                onClick={() => setOpen(false)}
                aria-label="Закрыть меню"
              >
                <span className="block h-[1px] w-3 rotate-45 rounded-full bg-slate-800" />
                <span className="absolute block h-[1px] w-3 -rotate-45 rounded-full bg-slate-800" />
              </button>
            </div>
            <nav className="flex flex-1 flex-col gap-3 text-sm">
              {navItems.map((item) => (
                <button
                  key={item.id}
                  onClick={() => handleNavClick(item.id)}
                  className="rounded-xl px-3 py-2 text-left text-slate-700 transition hover:bg-slate-100"
                >
                  {item.label}
                </button>
              ))}
            </nav>
            <button className="mt-4 w-full rounded-md border-2 border-[#ff6adf] bg-white px-4 py-2.5 text-[0.8rem] font-semibold tracking-wide text-slate-900 shadow-sm shadow-[#ff6adf]/15 transition hover:bg-[#ff6adf]/10 hover:shadow-md">
              ВОЙТИ
            </button>
          </div>
        </div>
      )}
    </header>
  );
}

