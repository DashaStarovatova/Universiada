// // src/pages/BlobPage.tsx
// import { useEffect } from "react";

// export function BlobPage() {
//   // Отключаем скролл
//   useEffect(() => {
//     document.body.style.overflow = "hidden";
//     return () => {
//       document.body.style.overflow = "auto";
//     };
//   }, []);

//   return (
//     <div className="relative min-h-screen bg-white overflow-hidden">
      
//       {/* ОГРОМНАЯ КЛЯКСА на весь экран */}
//       <div className="fixed inset-0 flex items-center justify-center pointer-events-none">
//         <div 
//           className="gradient-blob"
//           style={{
//             width: "90vw",
//             height: "90vh",
//             filter: "blur(60px)",
//             opacity: 0.9,
//           }}
//         />
//       </div>
      
//       {/* Дополнительные кляксы для объема */}
//       <div className="fixed inset-0 pointer-events-none">
//         <div 
//           className="gradient-blob"
//           style={{
//             width: "60vh",
//             height: "60vh",
//             top: "-20%",
//             right: "-20%",
//             filter: "blur(50px)",
//             opacity: 0.6,
//           }}
//         />
//         <div 
//           className="gradient-blob"
//           style={{
//             width: "50vh",
//             height: "50vh",
//             bottom: "-15%",
//             left: "-15%",
//             filter: "blur(45px)",
//             opacity: 0.5,
//           }}
//         />
//       </div>
      
//       {/* Контент поверх клякс */}
//       <div className="relative z-10 flex flex-col items-center justify-center min-h-screen text-center px-4">
//         <h1 className="text-4xl md:text-6xl font-bold text-slate-950 mb-4">
//           VI Экономическая<br />Универсиада
//         </h1>
//         <p className="text-lg md:text-xl text-slate-700 max-w-2xl mb-8">
//           Попробуй себя в роли мегарегулятора
//         </p>
        
//         <div className="flex gap-4">
//           <button 
//             onClick={() => window.location.href = "/"}
//             className="px-6 py-3 bg-slate-950 text-white rounded-full hover:bg-slate-800 transition"
//           >
//             На главную
//           </button>
//           <button 
//             onClick={() => window.open("https://forms.yandex.ru/ваша_ссылка", "_blank")}
//             className="px-6 py-3 bg-gradient-to-r from-[#ff6adf] to-[#6a8bff] text-white rounded-full hover:opacity-90 transition"
//           >
//             Принять участие
//           </button>
//         </div>
//       </div>
//     </div>
//   );
// }
