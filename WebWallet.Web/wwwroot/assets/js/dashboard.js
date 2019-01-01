$(function () {
    /*
         =====================================================================================================================

                            Income vs Investment Line Chart

         =====================================================================================================================
     */
    $.getJSON("https://localhost:5001/Authenticated/Dashboard/AllReports", function (reports) {
        reports = JSON.parse(reports);
        reports.push({
            CreatedOn: "2019-02-01T06:40:12.1278044Z",
            EndBalance: 18635,
            InvestmentsPerType: { "Крипто": 300, "Стокова борса": 200 },
            Name: "февруари 2019",
            SpendingsPerCategory: { "Шопинг": "80%", "Подаръци": "20%" },
            SpendingsPerMainCategory: {"Шопинг":"100%"},
            TotalIncome: 1260,
            TotalInvested: 500,
            TotalSpendings: 310
        });
        reports.push({
            CreatedOn: "2019-03-01T06:40:12.1278044Z",
            EndBalance: 18635,
            InvestmentsPerType: { "Крипто": 380, "Стокова борса": 700 },
            Name: "март 2019",
            SpendingsPerCategory: { "Шопинг": "70%", "Подаръци": "30%" },
            SpendingsPerMainCategory: { "Шопинг": "100%" },
            TotalIncome: 4260,
            TotalInvested: 1080,
            TotalSpendings: 1280
        });
        reports.push({
            CreatedOn: "2019-04-01T06:40:12.1278044Z",
            EndBalance: 10635,
            InvestmentsPerType: { "Вила": 300, "Апартамент": 200 },
            Name: "април 2019",
            SpendingsPerCategory: { "Шопинг": "50%", "Подаръци": "50%" },
            SpendingsPerMainCategory: { "Шопинг": "100%" },
            TotalIncome: 6260,
            TotalInvested: 500,
            TotalSpendings: 1330
        });

        am4core.useTheme(am4themes_animated);
        var incomeVsExpence = am4core.create("incomeVsExpence", am4charts.XYChart);
        //incomeVsExpence.language.locale = am4lang_bg_BG;
        incomeVsExpence.colors.step = 2;
        incomeVsExpence.data = reports;
        console.log(incomeVsExpence.data);

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

        /*
         =====================================================================================================================

                            Total Invested vs Total Income Bar Chart

         =====================================================================================================================
         */

        var investedVsIncome = am4core.create("investedVsIncome", am4charts.XYChart);
        investedVsIncome.data = reports;

        // Create axes
        var categoryAxis = investedVsIncome.xAxes.push(new am4charts.CategoryAxis());
        categoryAxis.dataFields.category = "Name";
        categoryAxis.renderer.grid.template.location = 0;
        categoryAxis.renderer.minGridDistance = 30;

        var valueAxis = investedVsIncome.yAxes.push(new am4charts.ValueAxis());
        valueAxis.title.text = "Сума в лева";
        valueAxis.title.fontWeight = 800;

        // Create series
        var series = investedVsIncome.series.push(new am4charts.ColumnSeries());
        series.dataFields.valueY = "TotalIncome";
        series.dataFields.categoryX = "Name";
        series.clustered = false;
        series.tooltipText = "Приходи за {categoryX} : [bold]{valueY}[/]";

        var series2 = investedVsIncome.series.push(new am4charts.ColumnSeries());
        series2.dataFields.valueY = "TotalInvested";
        series2.dataFields.categoryX = "Name";
        series2.clustered = false;
        series2.columns.template.width = am4core.percent(50);
        series2.tooltipText = "Инвестиции за {categoryX} : [bold]{valueY}[/]";

        investedVsIncome.cursor = new am4charts.XYCursor();
        investedVsIncome.cursor.lineX.disabled = true;
        investedVsIncome.cursor.lineY.disabled = true;

        /*
         =====================================================================================================================

                            Spendings for current month Pie Chart

         =====================================================================================================================
         */

        // Create chart instance
        var spendingsPerCategoryCurrentMonthChart = am4core.create("spendingsPerCategoryCurrentMonth", am4charts.PieChart);
        var selected = -1;
        var generateCategoryChartData = function () {
            var spendCurrMonthData = [];
            var index = 0;
            reports.forEach(function selectData(rep) {
                var keys;
                if (selected == index) {
                    keys = Object.keys(rep.SpendingsPerCategory);
                    keys.forEach(function getSubCategoryData(k) {
                        spendCurrMonthData.push({
                            category: k,
                            percent: rep.SpendingsPerCategory[k],
                            color: spendingsPerCategoryCurrentMonthChart.colors.getIndex(index),
                            pulled: true,
                            id: index
                        });
                    });
                } else {
                    keys = Object.keys(rep.SpendingsPerMainCategory);
                    keys.forEach(function getCategoryData(k) {
                        spendCurrMonthData.push({
                            category: k,
                            percent: rep.SpendingsPerMainCategory[k],
                            color: spendingsPerCategoryCurrentMonthChart.colors.getIndex(index),
                            id: index
                        });
                    });
                }

                index += 1;
            });

            return spendCurrMonthData;
        };

        spendingsPerCategoryCurrentMonthChart.data = generateCategoryChartData();

        // Add and configure Series
        var pieSeries = spendingsPerCategoryCurrentMonthChart.series.push(new am4charts.PieSeries());
        pieSeries.dataFields.value = "percent";
        pieSeries.dataFields.category = "type";
        pieSeries.slices.template.propertyFields.fill = "color";
        pieSeries.slices.template.propertyFields.isActive = "pulled";
        pieSeries.slices.template.strokeWidth = 0;

        pieSeries.slices.template.events.on("hit", function (event) {
            if (event.target.dataItem.dataContext.id != undefined) {
                selected = event.target.dataItem.dataContext.id;
            } else {
                selected = undefined;
            }
            spendingsPerCategoryCurrentMonthChart.data = generateCategoryChartData();
        });

    });
});