import { useState, useRef, useEffect } from "react";
import { Link } from "react-router-dom";
import {
  LineChart,
  Line,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  Legend,
  ResponsiveContainer,
  ReferenceLine
} from "recharts";
import chartData from "./data/chartData.json";

export function Cabinet() {
  const [data, setData] = useState(chartData.data);
  const [loading, setLoading] = useState(false);
  const [dropdownOpen, setDropdownOpen] = useState(false);
  const dropdownRef = useRef<HTMLDivElement>(null);
  const [teamName, setTeamName] = useState("Экономические волки");

  // Расчет среднего квадратичного отклонения инфляции от цели (4%)
  const calculateStdDev = () => {
    const target = 4;
    const deviations = data.map(item => Math.pow(item.inflation - target, 2));
    const sum = deviations.reduce((acc, val) => acc + val, 0);
    const variance = sum / data.length;
    return Math.sqrt(variance).toFixed(1);
  };

  const stdDev = calculateStdDev();

  // Текущие значения (последние в массиве) с округлением до 1 знака
  const currentKeyRate = data[data.length - 1]?.keyRate
    ? Number(data[data.length - 1].keyRate).toFixed(1)
    : "0.0";

  const currentInflation = data[data.length - 1]?.inflation
    ? Number(data[data.length - 1].inflation).toFixed(1)
    : "0.0";

  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (dropdownRef.current && !dropdownRef.current.contains(event.target as Node)) {
        setDropdownOpen(false);
      }
    };
    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, []);


  // Добавьте в начало компонента (после useState)
  const [rateValue, setRateValue] = useState("");
  const [selectedFile, setSelectedFile] = useState<File | null>(null);
  const [uploading, setUploading] = useState(false);

  // Даты доступности загрузки файлов (пример: 1–15 апреля 2026)
  const canUploadFiles = (): boolean => {
    const today = new Date();
    const start = new Date(2026, 3, 1);  // 1 апреля 2026
    const end = new Date(2026, 3, 15);   // 15 апреля 2026
    return today >= start && today <= end;
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!rateValue) {
      alert("Введите значение ставки");
      return;
    }
    if (canUploadFiles() && !selectedFile) {
      alert("Прикрепите файл с аналитическим обзором");
      return;
    }
    setUploading(true);
    // TODO: отправка данных на бэкенд
    console.log("Ставка:", rateValue);
    console.log("Файл:", selectedFile?.name);
    setTimeout(() => {
      alert("Данные отправлены");
      setUploading(false);
      setRateValue("");
      setSelectedFile(null);
      const fileInput = document.getElementById("file-upload") as HTMLInputElement;
      if (fileInput) fileInput.value = "";
    }, 1000);
  };


  if (loading) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <div className="text-center">
          <div className="text-2xl font-bold text-[#ff6adf]">Загрузка данных...</div>
        </div>
      </div>
    );
  }



  // Добавьте в начало компонента (после других useState)
  const [downloading, setDownloading] = useState(false);

  const handleDownloadData = async () => {
    setDownloading(true);
    try {
      // TODO: заменить на реальный API-запрос к бэкенду
      const response = await fetch("/api/download-data", {
        method: "GET",
        headers: {
          "Authorization": `Bearer ${localStorage.getItem("token")}`,
        },
      });

      if (!response.ok) throw new Error("Ошибка загрузки");

      const blob = await response.blob();
      const url = URL.createObjectURL(blob);
      const a = document.createElement("a");
      a.href = url;
      a.download = `economic_data_${new Date().toISOString().slice(0, 19)}.xlsx`;
      document.body.appendChild(a);
      a.click();
      document.body.removeChild(a);
      URL.revokeObjectURL(url);
    } catch (error) {
      console.error("Ошибка:", error);
      alert("Не удалось загрузить данные");
    } finally {
      setDownloading(false);
    }
  };

  const infoSlides = [
    {
      id: 1,
      text: "Отправка доступна с 9:00 до 18:00 МСК согласно календарю олимпиады."
    },
    {
      id: 2,
      text: "Новые данные появятся после расчёта влияния вашей ставки на экономику."
    },
    {
      id: 3,
      text: "Прогресс — на графике. Ваша цель: минимизировать среднеквадратическое отклонение инфляции от цели."
    }
  ];
  
  const [currentSlide, setCurrentSlide] = useState(0);
  
  const nextSlide = () => {
    setCurrentSlide((prev) => (prev + 1) % infoSlides.length);
  };
  
  const prevSlide = () => {
    setCurrentSlide((prev) => (prev - 1 + infoSlides.length) % infoSlides.length);
  };





  return (
    <div className="min-h-screen bg-white">
      {/* Фоновая клякса */}
      <div className="pointer-events-none fixed inset-0">
        <div className="gradient-blob fixed left-[50%] -translate-x-1/2 bottom-[-40%] h-30 w-30 md:h-[100rem] md:w-[110rem]" />
      </div>

      {/* Верхняя панель с кнопками */}
      <div className="flex justify-between items-center px-24 pt-8 pb-4">
        <Link
          to="/"
          className="cursor-pointer gradient-border_navig px-5 py-2 text-[0.85rem] font-semibold tracking-wide text-slate-900 transition hover:shadow-md active:translate-y-0"
        >
          ⟵ ЭКОНОМИЧЕСКАЯ УНИВЕРСИАДА 2026
        </Link>

        <button
          onClick={() => window.location.href = "/"}
          className="cursor-pointer uppercase gradient-border_navig px-5 py-2 text-[0.85rem] font-semibold tracking-wide text-slate-900 transition hover:shadow-md active:translate-y-0"
        >
          ВЫЙТИ
        </button>
      </div>


{/* Блок с пояснением и формами отправки */}
<div className="relative z-10 w-full max-w-[1180px] mx-auto mb-8 mt-20">
  <div className="rounded-xl p-6">
    <div className="flex flex-col md:flex-row gap-8">
      
{/* Левая колонка — текст на всю высоту */}
<div className="flex-1">
  <div className="bg-transparent border border-white/50 backdrop-blur-sm rounded-xl p-6 shadow-sm h-full flex flex-col">
    
    {/* Текст — растянут по вертикали */}
    <div className="flex-1 flex flex-col justify-between">
      <div>
        <p className="text-[1.0rem] font-semibold leading-snug text-slate-900">
          Отправка доступна с 9:00 до 18:00 МСК согласно календарю олимпиады.
        </p>
      </div>
      
      <div>
        <p className="text-[1.0rem] font-semibold leading-snug text-slate-900">
          Новые данные появятся после расчёта влияния вашей ставки на экономику.
        </p>
      </div>
      
      <div>
        <p className="text-[1.0rem] font-semibold leading-snug text-slate-900">
          Прогресс — на графике. Ваша цель: минимизировать среднеквадратическое отклонение инфляции от цели.
        </p>
      </div>
    </div>
    
  </div>
</div>

      {/* Правая колонка — ОДИН прямоугольник с двумя формами */}
      <div className="flex-1">
        <div className="bg-transparent border border-white/50 backdrop-blur-sm rounded-xl p-5 shadow-sm">
          <form onSubmit={handleSubmit} className="space-y-6">
            
            {/* Форма отправки ставки */}
            <div>
              <h3 className="text-lg uppercase font-bold text-black mb-2">Отправить решение по ставке</h3>
              <p className="text-sm font-medium text-slate-600 mb-4">Введите предлагаемое значение ключевой ставки (в %)</p>
              <input
                type="number"
                min="0"
                max="100"
                value={rateValue}
                onChange={(e) => setRateValue(e.target.value)}
                placeholder="Например, 11.5"
                required
                className="w-full px-4 py-3 rounded-full border border-slate-300 focus:outline-none focus:ring-2 focus:ring-[#8fff8f] text-center text-black text-base
                  [appearance:textfield] 
                  [&::-webkit-outer-spin-button]:appearance-none 
                  [&::-webkit-inner-spin-button]:appearance-none"
              />
            </div>

            {/* Разделительная линия */}
            <div className="border-t border-white/90 my-4" />

            {/* Форма загрузки файла */}
            <div>
              <h3 className="text-lg uppercase font-bold text-black mb-2">Загрузить обзор ДКП / методологию</h3>
              <p className="text-sm font-medium text-slate-600 mb-4">
                {canUploadFiles()
                  ? "Прикрепите файл в формате PDF или DOCX"
                  : "Загрузка файлов доступна с 1 по 15 апреля 2026 года"}
              </p>
              {canUploadFiles() ? (
                <input
                  id="file-upload"
                  type="file"
                  accept=".pdf,.docx"
                  onChange={(e) => setSelectedFile(e.target.files?.[0] || null)}
                  className="block w-full text-sm text-slate-700
                    file:mr-4 file:py-2 file:px-4
                    file:rounded-full file:border-0
                    file:text-sm file:font-semibold
                    file:bg-slate-950 file:text-white
                    hover:file:bg-slate-800
                    cursor-pointer"
                />
              ) : (
                <div className="w-full text-center text-slate-500 py-2 border border-dashed border-slate-300 rounded-full">
                  Загрузка сейчас недоступна
                </div>
              )}
            </div>

            {/* Кнопки */}
            <div className="flex flex-col sm:flex-row justify-center items-center gap-4 mt-6">
              <button
                type="button"
                onClick={handleDownloadData}
                disabled={downloading}
                className="group cursor-pointer inline-flex items-center gap-2 rounded-full border border-white/70 bg-white/70 px-6 py-3 text-[1rem] font-bold text-slate-900 shadow-sm backdrop-blur-md transition hover:-translate-y-0.5 hover:bg-white hover:shadow-md active:translate-y-0 disabled:opacity-50 disabled:cursor-not-allowed"
              >
                {downloading ? "Загрузка..." : "Скачать данные"}
                <span className="inline-block h-2 w-2 rounded-full bg-[#8232a1] shadow-[0_0_18px_rgba(143,255,143,0.8)] transition group-hover:scale-110" />
              </button>

              <button
                type="submit"
                disabled={uploading}
                className="group cursor-pointer inline-flex items-center gap-2 rounded-full bg-slate-950 px-8 py-3 text-[1rem] font-bold text-white shadow-lg shadow-slate-950/20 transition hover:-translate-y-0.5 hover:bg-slate-900 active:translate-y-0 disabled:opacity-50 disabled:cursor-not-allowed"
              >
                {uploading ? "Отправка..." : "Отправить"}
                <span className="inline-block h-2 w-2 rounded-full bg-[#8fff8f] shadow-[0_0_18px_rgba(143,255,143,0.8)] transition group-hover:scale-110" />
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</div>

      <div className="relative mx-auto max-w-[1200px] px-4 py-8 md:py-12">
        {/* Белый контейнер с графиком и информацией */}
        <div className="w-full max-w-[1180px] mx-auto rounded-xl bg-white/95 shadow-lg shadow-slate-950/50 overflow-hidden">

          {/* Заголовок с именем команды */}
          <h2 className="text-xl font-bold text-slate-950 text-center mb-6 pt-8">
            Мониторинг результатов команды {teamName}
          </h2>

          {/* График + дополнительная информация в одной строке */}
          <div className="flex flex-col lg:flex-row gap-6 p-6">

            {/* График (слева) */}
            <div className="flex-1">
              <ResponsiveContainer width="100%" height={380}>
                <LineChart
                  data={data}
                  margin={{ top: 5, right: 30, left: 20, bottom: 10 }}
                >
                  <CartesianGrid strokeDasharray="3 3" stroke="#e2e8f0" />
                  <XAxis
                    dataKey="month"
                    tick={{ fontSize: 12, fill: "#000000" }}
                    interval={2}
                  />
                  <YAxis
                    tick={{ fontSize: 12, fill: "#000000" }}
                    domain={[0, 18]}
                    label={{
                      value: "Проценты (%)",
                      angle: -90,
                      position: "insideLeft",
                      style: { fill: "#000000", fontSize: 12 }
                    }}
                  />
                  <Tooltip
                    contentStyle={{
                      backgroundColor: "white",
                      borderRadius: "12px",
                      border: "1px solid #e2e8f0",
                      boxShadow: "0 4px 12px rgba(0,0,0,0.1)"
                    }}
                    formatter={(value, name) => {
                      if (name === "target") return null;
                      const labels: Record<string, string> = {
                        keyRate: "Ключевая ставка",
                        inflation: "Инфляция"
                      };
                      return [`${value}%`];
                    }}
                  />
                  <Legend
                    verticalAlign="bottom"
                    height={36}
                    formatter={(value) => {
                      const labels: Record<string, string> = {
                        keyRate: "Ключевая ставка",
                        inflation: "Инфляция",
                        target: "Цель по инфляции"
                      };
                      return <span className="text-sm text-slate-700">{labels[value]}</span>;
                    }}
                  />

                  <Line
                    type="monotone"
                    dataKey="keyRate"
                    stroke="#5db06f"
                    strokeWidth={3}
                    dot={{ r: 2, fill: "#5db06f", strokeWidth: 2 }}
                    activeDot={{ r: 6 }}
                  />

                  <Line
                    type="monotone"
                    dataKey="inflation"
                    stroke="#783baa"
                    strokeWidth={3}
                    dot={{ r: 2, fill: "#783baa", strokeWidth: 2 }}
                    activeDot={{ r: 6 }}
                  />

                  <Line
                    type="monotone"
                    dataKey="target"
                    stroke="#000000"
                    strokeWidth={2}
                    dot={false}
                    activeDot={false}
                  />
                </LineChart>
              </ResponsiveContainer>
            </div>

            {/* Дополнительная информация (справа, столбиком) */}
            <div className="w-full lg:w-64 flex flex-col gap-4">
              <div className="bg-transparent rounded-xl p-4 text-center">
                <div className="text-5xl font-bold text-[#5db06f]">{currentKeyRate}%</div>
                <div className="text-sm text-slate-500 mt-1">Ключевая ставка</div>
              </div>
              <div className="bg-transparent rounded-xl p-4 text-center">
                <div className="text-5xl font-bold text-[#783baa]">{currentInflation}%</div>
                <div className="text-sm text-slate-500 mt-1">Инфляция</div>
              </div>
              <div className="bg-transparent rounded-xl p-4 text-center">
                <div className="text-5xl font-bold text-slate-900">{stdDev}</div>
                <div className="text-sm text-slate-500 mt-1">Среднее квадратичное отклонение от цели</div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}