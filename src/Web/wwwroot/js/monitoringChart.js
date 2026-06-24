(function () {
    let chartInstance = null;

    function drawChart() {
        const canvas = document.getElementById('monitoringCanvas');
        if (!canvas) return;

        const dataAttr = canvas.getAttribute('data-chart');
        if (!dataAttr) return;

        let chartData;
        try {
            chartData = JSON.parse(dataAttr);
        } catch (e) {
            return;
        }

        if (!chartData || chartData.length === 0) return;

        // Уничтожаем старый график
        if (chartInstance) {
            chartInstance.destroy();
            chartInstance = null;
        }

        const labels = chartData.map(d => d.Period);
        const keyRates = chartData.map(d => d.KeyRate);
        const targets = chartData.map(d => d.Target);
        // Берём данные из chartData
        const inflation_list = chartData.map(d => d.Inflation);
        const yValues = chartData.map(d => Math.exp(d.Inflation) * 100);
        const zList = [101.119, 100.294, 99.454];
        const inflations = [];

        for (let i = 0; i < yValues.length; i++) {
            let product;

            if (i === 0) product = zList[0] * zList[1] * zList[2] * yValues[i];
            else if (i === 1) product = zList[1] * zList[2] * yValues[i - 1] * yValues[i];
            else if (i === 2) product = zList[2] * yValues[i - 2] * yValues[i - 1] * yValues[i];
            else product = yValues[i - 3] * yValues[i - 2] * yValues[i - 1] * yValues[i];

            inflations.push(product / Math.pow(100, 3) - 100);
        }


        const minDataValue = Math.min(...keyRates, ...inflations);
        const maxDataValue = Math.max(...keyRates, ...inflations);
        let yMin = Math.floor(minDataValue - 2);
        if (yMin < 0) yMin = 0;
        const yMax = Math.ceil(maxDataValue + 2);

        let legendFontSize, xFontSize, yFontSize, tooltipFontSize;
        const width = window.innerWidth;
        if (width < 480) {
            legendFontSize = 8; xFontSize = 8; yFontSize = 8; tooltipFontSize = 10;
        } else if (width < 768) {
            legendFontSize = 8; xFontSize = 8; yFontSize = 8; tooltipFontSize = 10;
        } else if (width < 1024) {
            legendFontSize = 10; xFontSize = 10; yFontSize = 10; tooltipFontSize = 12;
        } else if (width < 1280) {
            legendFontSize = 10; xFontSize = 10; yFontSize = 10; tooltipFontSize = 12;
        } else {
            legendFontSize = 15; xFontSize = 15; yFontSize = 15; tooltipFontSize = 17;
        }

        const ctx = canvas.getContext('2d');
        chartInstance = new Chart(ctx, {
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
                        titleFont: { size: tooltipFontSize, weight: 'bold' },
                        displayColors: false,
                        borderColor: '#747373',
                        borderWidth: 0.5,
                        bodyFont: { weight: 'bold', size: tooltipFontSize },
                        callbacks: {
                            label: function (context) {
                                if (context.dataset.label.includes('Цель')) return null;
                                return context.raw.toFixed(1) + '%';
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
                            font: { size: legendFontSize, color: '#000000' },
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
                scales: {
                    y: {
                        min: yMin,
                        max: yMax,
                        grid: { color: 'rgba(0, 0, 0, 0.08)', lineWidth: 1, drawBorder: true, borderDash: [4, 4] },
                        ticks: { color: '#000000', font: { size: yFontSize }, stepSize: 2, callback: function (value) { return value + '%'; } }
                    },
                    x: {
                        grid: { color: 'rgba(0, 0, 0, 0.08)', lineWidth: 1 },
                        ticks: { font: { size: xFontSize }, color: '#000000' }
                    }
                },
                interaction: { mode: 'nearest', axis: 'x', intersect: false },
                elements: { line: { tension: 0.3 }, point: { hoverRadius: 6, radius: 4 } }
            }
        });
    }

    // 1. Рисуем при загрузке
    // if (document.readyState === 'loading') {
    //     document.addEventListener('DOMContentLoaded', drawChart);
    // } else {
    //     drawChart();
    // }

    // 2. Следим за изменением атрибута data-chart (когда Blazor обновляет данные)
    const observer = new MutationObserver(function (mutations) {
        mutations.forEach(function (mutation) {
            if (mutation.type === 'attributes' && mutation.attributeName === 'data-chart') {
                console.log("Данные изменились, перерисовываю график");
                drawChart();
            }
        });
    });

    // Наблюдаем за canvas
    const canvas = document.getElementById('monitoringCanvas');
    if (canvas) {
        observer.observe(canvas, { attributes: true, attributeFilter: ['data-chart'] });
    }

    // 3. Также следим за появлением canvas в DOM (на случай пересоздания)
    const bodyObserver = new MutationObserver(function () {
        const newCanvas = document.getElementById('monitoringCanvas');
        if (newCanvas && !newCanvas.hasObserver) {
            newCanvas.hasObserver = true;
            observer.observe(newCanvas, { attributes: true, attributeFilter: ['data-chart'] });
            drawChart();
        }
    });

    bodyObserver.observe(document.body, { childList: true, subtree: true });

    // 4. При изменении размера окна
    window.addEventListener('resize', function () {
        setTimeout(drawChart, 100);
    });


})();