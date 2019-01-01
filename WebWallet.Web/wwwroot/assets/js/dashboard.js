$(function () {
    /*
        Income vs Expence Chart
    =======================================================
     */
    $.getJSON("https://localhost:5001/Authenticated/Dashboard/AllReports", function (reports) {
        am4core.useTheme(am4themes_animated);
        var incomeVsExpence = am4core.create("incomeVsExpence", am4charts.XYChart);
        //incomeVsExpence.language.locale = am4lang_bg_BG;
        incomeVsExpence.colors.step = 2;
        incomeVsExpence.data = JSON.parse(reports);
        incomeVsExpence.dateFormatter.dateFormat = '{dateX.toLocaleDateString()}';

        var incomeVsExpenceDateAxis = incomeVsExpence.xAxes.push(new am4charts.DateAxis());
        incomeVsExpenceDateAxis.renderer.minGridDistance = 50;

        // Create series
        function createAxisAndSeries(chart, field, name, opposite, bullet) {
            var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());

            var series = chart.series.push(new am4charts.LineSeries());
            series.dataFields.valueY = field;
            series.dataFields.dateX = "CreatedOn";
            series.strokeWidth = 2;
            series.yAxis = valueAxis;
            series.name = name;
            series.tooltipText = "{name}: [bold]{valueY}[/]";
            series.tensionX = 0.8;

            var interfaceColors = new am4core.InterfaceColorSet();

            switch (bullet) {
                case "triangle":
                    var bullet = series.bullets.push(new am4charts.Bullet());
                    bullet.width = 12;
                    bullet.height = 12;
                    bullet.horizontalCenter = "middle";
                    bullet.verticalCenter = "middle";

                    var triangle = bullet.createChild(am4core.Triangle);
                    triangle.stroke = interfaceColors.getFor("background");
                    triangle.strokeWidth = 2;
                    triangle.direction = "top";
                    triangle.width = 12;
                    triangle.height = 12;
                    break;
                case "rectangle":
                    var bullet = series.bullets.push(new am4charts.Bullet());
                    bullet.width = 10;
                    bullet.height = 10;
                    bullet.horizontalCenter = "middle";
                    bullet.verticalCenter = "middle";

                    var rectangle = bullet.createChild(am4core.Rectangle);
                    rectangle.stroke = interfaceColors.getFor("background");
                    rectangle.strokeWidth = 2;
                    rectangle.width = 10;
                    rectangle.height = 10;
                    break;
                default:
                    var bullet = series.bullets.push(new am4charts.CircleBullet());
                    bullet.circle.stroke = interfaceColors.getFor("background");
                    bullet.circle.strokeWidth = 2;
                    break;
            }

            valueAxis.renderer.line.strokeOpacity = 1;
            valueAxis.renderer.line.strokeWidth = 2;
            valueAxis.renderer.line.stroke = series.stroke;
            valueAxis.renderer.labels.template.fill = series.stroke;
            valueAxis.renderer.opposite = opposite;
            valueAxis.renderer.grid.template.disabled = true;
        }

        createAxisAndSeries(incomeVsExpence, "TotalIncome", "Приходи", true, "circle");
        createAxisAndSeries(incomeVsExpence, "TotalSpendings", "Разходи", true, "rectangle");

        // Add legend
        incomeVsExpence.legend = new am4charts.Legend();

        // Add cursor
        incomeVsExpence.cursor = new am4charts.XYCursor();
    });
});