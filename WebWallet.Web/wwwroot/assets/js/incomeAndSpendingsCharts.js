$(function () {
    $.getJSON("https://webwallet.azurewebsites.net/Authenticated/Report/AllReports", function (reports) {
        reports = JSON.parse(reports);
        reports.push({
            CreatedOn: "2019-02-02T15:27:11.0196374Z",
            EndBalance: 16035,
            InvestmentsPerType: { Crypto: 5100 },
            Name: "февруари 2019",
            SpendingsPerCategory: [
                {
                    Amount: 253,
                    CategoryName: "Храна и Напитки",
                    SubCategories: {
                        "Храни и напитки друго": 225,
                        "Хранителни продукти": 28
                    }
                },
                {
                    Amount: 734,
                    CategoryName: "Шопинг",
                    SubCategories: { "Дрехи и обувки": 534, "Шопинг друго": 200 }
                }
            ],
            TotalIncome: 1865,
            TotalInvested: 800,
            TotalSpendings: 987
        });

        reports.push({
            CreatedOn: "2019-03-02T15:27:11.0196374Z",
            EndBalance: 1750,
            InvestmentsPerType: { Crypto: 5100 },
            Name: "март 2019",
            SpendingsPerCategory: [
                {
                    Amount: 553,
                    CategoryName: "Храна и Напитки",
                    SubCategories: {
                        "Храни и напитки друго": 425,
                        "Хранителни продукти": 128
                    }
                },
                {
                    Amount: 234,
                    CategoryName: "Шопинг",
                    SubCategories: { "Дрехи и обувки": 134, "Шопинг друго": 100 }
                }
            ],
            TotalIncome: 1365,
            TotalInvested: 100,
            TotalSpendings: 700
        });
        reports.push({
            CreatedOn: "2019-04-02T15:27:11.0196374Z",
            EndBalance: 19035,
            InvestmentsPerType: { Crypto: 5100 },
            Name: "април 2019",
            SpendingsPerCategory: [
                {
                    Amount: 450,
                    CategoryName: "Храна и Напитки",
                    SubCategories: {
                        "Храни и напитки друго": 220,
                        "Хранителни продукти": 230
                    }
                },
                {
                    Amount: 234,
                    CategoryName: "Шопинг",
                    SubCategories: { "Дрехи и обувки": 184, "Шопинг друго": 50 }
                }
            ],
            TotalIncome: 1365,
            TotalInvested: 300,
            TotalSpendings: 687
        });

        am4core.useTheme(am4themes_animated);

        /*
             =====================================================================================================================

                                Income vs Investment Line Chart

             =====================================================================================================================
         */
        var incomeVsExpence = am4core.create("incomeVsExpence", am4charts.XYChart);
        //incomeVsExpence.language.locale = am4lang_bg_BG;
        incomeVsExpence.colors.step = 2;
        incomeVsExpence.data = reports;

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
        am4core.useTheme(am4themes_animated);
        // Create chart instance
        var spendingsPerCategoryCurrentMonthChart = am4core.create("spendingsPerCategoryCurrentMonth", am4charts.PieChart);
        var selected;
        var mainCatColors = {};
        // Generate Data
        var generateCategoryChartData = function () {
            var currentReport = reports[reports.length - 1];

            var keys = Object.keys(currentReport.SpendingsPerCategory);
            var spendingsData = [];
            for (var i = 0; i < keys.length; i++) {
                if (selected == i) {
                    var subCategories = currentReport.SpendingsPerCategory[i].SubCategories;
                    var subKeys = Object.keys(subCategories);
                    var mainCat = currentReport.SpendingsPerCategory[i];

                    for (var j = 0; j < subKeys.length; j++) {
                        var subCatKey = subKeys[j];
                        var subCatVal = mainCat.SubCategories[subCatKey];

                        spendingsData.push({
                            category: subCatKey,
                            percent: subCatVal / currentReport.TotalSpendings * 100,
                            color: mainCatColors[mainCat.CategoryName],
                            pulled: true
                        });
                    }
                } else {
                    var color = spendingsPerCategoryCurrentMonthChart.colors.getIndex(i);
                    var category = currentReport.SpendingsPerCategory[i].CategoryName;
                    var percent = currentReport.SpendingsPerCategory[i].Amount / currentReport.TotalSpendings * 100;
                    var id = i;
                    mainCatColors[category] = color;

                    spendingsData.push({
                        category,
                        percent,
                        color,
                        id
                    });
                }
            }

            return spendingsData;
        }

        spendingsPerCategoryCurrentMonthChart.data = generateCategoryChartData();

        // Add and configure Series
        var pieSeries = spendingsPerCategoryCurrentMonthChart.series.push(new am4charts.PieSeries());
        pieSeries.dataFields.value = "percent";
        pieSeries.dataFields.category = "category";
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

        /*
      =====================================================================================================================

                         Spendings for all months Pie Chart

      =====================================================================================================================
      */

        // Create chart instance
        var spendingsPerCategoryChart = am4core.create("spendingsPerCategoryAllTime", am4charts.PieChart);
        var selectedAllTime;
        var mainCatColorsAllTime = {};
        // Generate Data
        var generateCategoryChartDataAllTime = function () {
            var allReports = {
                TotalSpendings: 0,
                SpendingsPerCategory: []
            };
            reports.forEach(function (report) {
                for (var i = 0; i < report.SpendingsPerCategory.length; i++) {
                    var spendings = report.SpendingsPerCategory[i];
                    var categoryName = spendings.CategoryName;

                    if (!allReports.SpendingsPerCategory.some(x => x.CategoryName == categoryName)) {
                        allReports.SpendingsPerCategory.push({
                            CategoryName: spendings.CategoryName,
                            Amount: 0,
                            SubCategories: {}
                        });
                    }

                    allReports.TotalSpendings += spendings.Amount;

                    var spendingsSubCategoriesKeys = Object.keys(spendings.SubCategories);
                    var currentCategories = allReports.SpendingsPerCategory.find(x => x.CategoryName == categoryName);

                    spendingsSubCategoriesKeys.forEach(key => {
                        if (!currentCategories.SubCategories[key]) {
                            currentCategories.SubCategories[key] = 0;
                        }

                        currentCategories.SubCategories[key] += spendings.SubCategories[key];
                        currentCategories.Amount += spendings.SubCategories[key];
                    });
                }
            });

            var keys = Object.keys(allReports.SpendingsPerCategory);
            var spendingsData = [];
            for (var i = 0; i < keys.length; i++) {
                if (selectedAllTime == i) {
                    var mainCat = allReports.SpendingsPerCategory[keys[i]];
                    console.log(mainCat);
                    var subCategories = mainCat.SubCategories;
                    var subKeys = Object.keys(subCategories);

                    for (var j = 0; j < subKeys.length; j++) {
                        var subCatKey = subKeys[j];
                        var subCatVal = mainCat.SubCategories[subCatKey];

                        spendingsData.push({
                            category: subCatKey,
                            percent: subCatVal / allReports.TotalSpendings * 100,
                            color: mainCatColorsAllTime[mainCat.CategoryName],
                            pulled: true
                        });
                    }
                } else {
                    var color = spendingsPerCategoryCurrentMonthChart.colors.getIndex(i);
                    var category = allReports.SpendingsPerCategory[keys[i]].CategoryName;
                    var percent = allReports.SpendingsPerCategory[keys[i]].Amount / allReports.TotalSpendings * 100;
                    var id = i;
                    mainCatColorsAllTime[category] = color;

                    spendingsData.push({
                        category,
                        percent,
                        color,
                        id
                    });
                }
            }

            return spendingsData;
        }

        spendingsPerCategoryChart.data = generateCategoryChartDataAllTime();

        // Add and configure Series
        var pieSeries = spendingsPerCategoryChart.series.push(new am4charts.PieSeries());
        pieSeries.dataFields.value = "percent";
        pieSeries.dataFields.category = "category";
        pieSeries.slices.template.propertyFields.fill = "color";
        pieSeries.slices.template.propertyFields.isActive = "pulled";
        pieSeries.slices.template.strokeWidth = 0;

        pieSeries.slices.template.events.on("hit", function (event) {
            if (event.target.dataItem.dataContext.id != undefined) {
                selectedAllTime = event.target.dataItem.dataContext.id;
            } else {
                selectedAllTime = undefined;
            }
            spendingsPerCategoryChart.data = generateCategoryChartDataAllTime();
        });
        console.dir(reports[0]);
    });
});