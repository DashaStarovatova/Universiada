(function () {
    function initChart() {
        const canvas = document.getElementById('monitoringCanvas');
        if (!canvas) return;

        const dataAttr = canvas.getAttribute('data-chart');
        if (!dataAttr) return;

        const chartData = JSON.parse(dataAttr);

        const labels = chartData.map(d => d.Period);
        const keyRates = chartData.map(d => d.KeyRate);
        const inflations = chartData.map(d => d.Inflation);
        const targets = chartData.map(d => d.Target);

        const minDataValue = Math.min(...keyRates, ...inflations);
        const maxDataValue = Math.max(...keyRates, ...inflations);

        // Минимум: округляем вниз, вычитаем 1, но не меньше 0
        let yMin = Math.floor(minDataValue - 1);
        if (yMin < 0) yMin = 0;

        // Максимум: округляем вверх, прибавляем 1
        const yMax = Math.ceil(maxDataValue + 1);

        const width= window.innerWidth;
        if (width < 480) {
            legendFontSize = 7
            xFontSize = 7
            yFontSize = 7
            tooltipFontSize = 9
        } else if (width >= 480 & width < 768) {
            legendFontSize = 7
            xFontSize = 7
            yFontSize = 7
            tooltipFontSize = 9
        } else if (width >= 768 & width < 1024) {
            legendFontSize = 10
            xFontSize = 10
            yFontSize = 10
            tooltipFontSize = 12
        } else if (width >= 1024 & width < 1280) {
            legendFontSize = 10
            xFontSize = 10
            yFontSize = 10
            tooltipFontSize = 12
        } else if (width >= 1280 & width < 1440) {
            legendFontSize = 15
            xFontSize = 15
            yFontSize = 15
            tooltipFontSize = 17
        } else {
            legendFontSize = 15
            xFontSize = 15
            yFontSize = 15
            tooltipFontSize = 17
        }


        const ctx = canvas.getContext('2d');

        new Chart(ctx, {
            type: 'line',
            data: {
                labels: labels,
                datasets: [
                    {
                        label: 'Ключевая ставка',
                        data: keyRates,
                        borderColor: '#5db06f',
                        backgroundColor: 'transparent',
                        borderWidth: 3,
                        pointRadius: 2,
                        pointBackgroundColor: '#5db06f',
                        pointHoverBorderColor: '#ffffff',
                        pointHoverBorderWidth: 2,
                        pointBorderWidth: 2,
                        pointHoverRadius: 8,
                        tension: 0.3,
                        fill: false
                    },
                    {
                        label: 'Инфляция',
                        data: inflations,
                        borderColor: '#783baa',
                        backgroundColor: 'transparent',
                        borderWidth: 3,
                        pointRadius: 2,
                        pointBackgroundColor: '#783baa',
                        pointHoverBorderColor: '#ffffff',
                        pointHoverBorderWidth: 2,
                        pointBorderWidth: 2,
                        pointHoverRadius: 8,
                        tension: 0.3,
                        fill: false
                    },
                    {
                        label: 'Цель по инфляции (4%)',
                        data: targets,
                        borderColor: '#1a1a1a',
                        backgroundColor: 'transparent',
                        borderWidth: 2,
                        // borderDash: [8, 6],
                        pointRadius: 0,
                        pointHoverRadius: 0,
                        fill: false,
                        tension: 0.1
                    }
                ]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    tooltip: {
                        mode: 'index',
                        intersect: false,
                        backgroundColor: 'rgb(255, 255, 255)',
                        titleColor: '#000000',
                        cornerRadius: 8,
                        titleFont: {               // ← размер заголовка (периода)
                            size: tooltipFontSize,              // ← увеличь размер
                            weight: 'bold'
                        },
                        displayColors: false,
                        borderColor: '#747373',
                        borderWidth: 0.5,
                        bodyFont: {
                            weight: 'bold',   // ← жирный шрифт
                            size: tooltipFontSize          // ← можно чуть увеличить размер
                        },
                        callbacks: {
                            label: function (context) {
                                if (context.dataset.label.includes('Цель')) return null;

                                if (context.dataset.label.includes('Цель')) return null;
                                return context.raw.toFixed(1) + '%';

                                // Создаём span с цветом
                                const color = context.dataset.borderColor;
                                return ` ${context.raw}% `;
                            },
                            labelTextColor: function (context) {
                                if (context.dataset.label.includes('Цель')) return null;
                                return context.dataset.borderColor;
                            }
                        }
                    },

                    legend: {
                        position: 'bottom',
                        labels: {
                            usePointStyle: false,
                            pointStyle: 'line',
                            boxWidth: 20,
                            boxHeight: 1,
                            padding: 12,
                            font: {
                                size: legendFontSize,
                                color: '#000000',
                            },
                            generateLabels: function (chart) {
                                const datasets = chart.data.datasets;
                                return datasets.map((dataset, i) => ({
                                    text: dataset.label,
                                    fillStyle: dataset.borderColor,
                                    strokeStyle: dataset.borderColor,
                                    lineWidth: 3,
                                    hidden: false,
                                    index: i
                                }));
                            }
                        }
                    }
                },

                interaction: {
                    mode: 'index',
                    intersect: false,
                    axis: 'x'
                },
                scales: {
                    y: {
                        min: yMin,
                        max: yMax,
                        grid: {
                            color: 'rgba(0, 0, 0, 0.08)',
                            lineWidth: 1,
                            drawBorder: true,
                            borderDash: [4, 4]
                        },
                        ticks: {
                            color: '#000000',
                            font: { size: yFontSize },
                            stepSize: 2,
                            callback: function (value) {
                                return value + '%';
                            }
                        }
                    },
                    x: {
                        grid: {
                            color: 'rgba(0, 0, 0, 0.08)',
                            lineWidth: 1,

                        },
                        ticks: {
                            font: { size: xFontSize },
                            color: '#000000'
                        }
                    }
                },
                interaction: {
                    mode: 'nearest',
                    axis: 'x',
                    intersect: false
                },
                elements: {
                    line: {
                        tension: 0.3
                    },
                    point: {
                        hoverRadius: 6,
                        radius: 4
                    }
                }
            }
        });
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', initChart);
    } else {
        initChart();
    }







})();