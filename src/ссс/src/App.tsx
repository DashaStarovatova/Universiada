// import { Header } from "./components/Header";
import { Hero } from "./components/Hero";
import { Benefits } from "./components/Benefits";
import { Timeline } from "./components/Timeline";
import { Faq } from "./components/Faq";
import { Contact } from "./components/Contact";
import { NavigBar } from "./components/NavigBar";
import { BorderMarquee } from "./components/BorderMarquee";

import { useEffect } from "react";

function App() {
  <NavigBar />

  return (

    <div className="min-h-screen pb-10 text-slate-900">
      <div className="relative z-[9999]">
        <NavigBar />
      </div>
      <BorderMarquee />

      <div className="pointer-events-none fixed inset-0 -z-10">
        <div className="gradient-blob fixed left-[50%] -translate-x-1/2 bottom-[-40%] h-30 w-30 md:h-[100rem] md:w-[110rem]" />
      </div>

      {/* <Header /> */}
      <main className="pt-0">
        <Hero />
        <Benefits />
        <Timeline />
        <Faq />
        <Contact />
      </main>
    </div>
  );
}

export default App;

