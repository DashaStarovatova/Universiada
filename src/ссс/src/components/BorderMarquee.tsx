export function BorderMarquee() {
  const content = "| ЭКОНОМИЧЕСКАЯ УНИВЕРСИАДА — 2026 ";
  const repeatedContent = content.repeat(8);
  // Добавляем пробел между копиями
  const spacedContent = `${repeatedContent} ${repeatedContent} ${repeatedContent}`;
  
  return (
    <div className="fixed inset-0 pointer-events-none z-50">
      {/* Левая рамка */}
      <div className="absolute top-0 bottom-0 left-0 w-10 overflow-hidden">
        <div className="h-full flex flex-col items-center justify-start py-4">
          <div className="flex flex-col animate-marquee-vertical whitespace-nowrap text-xs font-mono font-medium tracking-wide text-[#000000]">
            {/* Одна непрерывная строка с пробелами между копиями */}
            <span className="writing-vertical-rotated leading-none">{spacedContent}</span>
          </div>
        </div>
      </div>

      {/* Правая рамка */}
      <div className="absolute top-0 bottom-0 right-0 w-10 overflow-hidden">
        <div className="h-full flex flex-col items-center justify-start py-4">
          <div className="flex flex-col animate-marquee-vertical-reverse whitespace-nowrap text-xs font-mono font-medium tracking-wide text-[#000000]">
            <span className="writing-vertical leading-none">{spacedContent}</span>
          </div>
        </div>
      </div>
    </div>
  );
}