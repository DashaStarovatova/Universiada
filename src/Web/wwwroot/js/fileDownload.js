(function() {
    window.downloadFile = function(fileName, content) {
        console.log("downloadFile вызван", fileName);
        
        try {
            // Добавляем BOM для русских символов
            const blob = new Blob(["\uFEFF" + content], { type: 'text/csv;charset=utf-8;' });
            const url = URL.createObjectURL(blob);
            const link = document.createElement('a');
            
            link.href = url;
            link.download = fileName;
            link.style.display = 'none';
            
            document.body.appendChild(link);
            link.click();
            
            // Даём время на скачивание
            setTimeout(() => {
                document.body.removeChild(link);
                URL.revokeObjectURL(url);
            }, 100);
            
            console.log("Скачивание инициировано", fileName);
            return true;
        } catch (error) {
            console.error("Ошибка в downloadFile:", error);
            return false;
        }
    };
    
    console.log("fileDownload.js загружен, window.downloadFile доступна");
})();