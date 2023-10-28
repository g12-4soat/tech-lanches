(function () {
    window.addEventListener("load", function () {
        setTimeout(function () {
            // Section 01 - Set url link 
            var logo = document.getElementsByClassName('link');
            logo[0].href = "/api-docs/index.html";
            logo[0].target = "_blank";

            // Section 02 - Set logo
            logo[0].children[0].alt = "logo";
            logo[0].children[0].src = "/SwaggerUI/images/apple-touch-icon.png";

            // Section 03 - Set 32x32 favicon
            var linkIcon32 = document.createElement('link');
            linkIcon32.type = 'image/png';
            linkIcon32.rel = 'icon';
            linkIcon32.href = '/SwaggerUI/images/favicon-32x32.png';
            linkIcon32.sizes = '32x32';
            document.getElementsByTagName('head')[0].appendChild(linkIcon32);

            // Section 03 - Set 16x16 favicon
            var linkIcon16 = document.createElement('link');
            linkIcon16.type = 'image/png';
            linkIcon16.rel = 'icon';
            linkIcon16.href = '/SwaggerUI/images/favicon-16x16.png';
            linkIcon16.sizes = '16x16';
            document.getElementsByTagName('head')[0].appendChild(linkIcon16);

            //var selecioneVersao = document.getElementsByClassName('select-label');
            //selecioneVersao[0].children[0].textContent = "Selecione uma versão";
        });
    });
})();