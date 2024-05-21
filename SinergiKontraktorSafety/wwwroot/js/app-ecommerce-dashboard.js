﻿"use strict"; !function () {
    let o, t, e, r, i, a, s = config.colors_label.primary; a = (isDarkStyle ? (o =
        config.colors_dark.cardColor, t = config.colors_dark.textMuted, e = config.colors_dark.headingColor, r =
        config.colors_dark.borderColor, i = config.colors_dark.chartBgColor, config.colors_dark) : (o = config.colors.cardColor,
            t = config.colors.textMuted, e = config.colors.headingColor, r = config.colors.borderColor, i =
            config.colors.chartBgColor, config.colors)).bodyColor; var n = document.querySelector("#totalProfitChart"), l = {
                chart:
                    { type: "bar", height: 260, parentHeightOffset: 0, stacked: !0, toolbar: { show: !1 } }, series: [{
                        name: "Revenue",
                        data: [29, 22, 25, 19, 29, 20, 35]
                    }, { name: "Transactions", data: ["", 16, 11, 16, "", 13, 10] }, {
                        name: "Total
Profit", data: ["", "", "", 14, "", 12, 12] }], plotOptions: { bar: { horizontal: !1, columnWidth: "35 % ", borderRadius:
10, startingShape: "rounded", endingShape: "rounded"
                    } }, dataLabels: { enabled: !1 }, stroke: {
                        curve: "smooth", width:
                        6, lineCap: "round", colors: [o]
                    }, legend: { show: !1 }, colors: [config.colors.primary, config.colors.success,
                        config.colors.secondary], grid: {
                            strokeDashArray: 8, borderColor: r, padding: {
                                top: -10, left: 15, right: -15, bottom:
                                -10
                            }
                        }, xaxis: {
                            categories: ["2015", "2016", "2017", "2018", "2019", "2020", "2021"], tickPlacement: "on", labels: {
                                show: !0, style: { fontSize: "0.75rem", fontFamily: "Inter", colors: t }
                            }, axisBorder: { show: !1 }, axisTicks: {
                                show:
                                !1
                            }
                        }, yaxis: { min: 0, max: 60, show: !0, tickAmount: 6, labels: { formatter: function (o) { return parseInt(o) + "K"
}, style: { fontSize: "0.75rem", fontFamily: "Inter", colors: t } } }, states: {
    hover: { filter: { type: "none" } },
    active: { filter: { type: "none" } }
}, responsive: [{
    breakpoint: 1441, options: {
        plotOptions: {
            bar: {
                columnWidth:
                    "50%"
            }
        }
    }
}, { breakpoint: 1200, options: { plotOptions: { bar: { columnWidth: "35%" } } } }, {
    breakpoint: 1025,
    options: { plotOptions: { bar: { columnWidth: "45%" } } }
}, {
    breakpoint: 767, options: {
        plotOptions: {
            bar: {
                columnWidth: "35%"
            }
        }
    }
}, { breakpoint: 555, options: { plotOptions: { bar: { columnWidth: "45%" } } } }, {
    breakpoint: 450, options: {
        chart: { height: 200, offsetX: -10 }, plotOptions: { bar: { columnWidth: "55%" } }, xaxis: {
            labels: { rotate: 315, rotateAlways: !0 }
        }
    }
}, {
    breakpoint: 360, options: {
        plotOptions: {
            bar: {
                columnWidth: "75%"
            }
        }
    }
}] }, n = (null !== n && new ApexCharts(n, l).render(), document.querySelector("#totalSalesDonutChart")), l = {
    chart: { height: 100, width: 110, parentHeightOffset: 0, type: "donut" }, labels: ["Comments", "Replies", "Shares"],
    series: [45, 10, 18, 27], colors: [config.colors.primary, config.colors.info, config.colors.warning,
    config.colors.danger], stroke: { width: 5, colors: o }, tooltip: { show: !1 }, dataLabels: {
        enabled: !1, formatter:
            function (o, t) { return parseInt(o) + "%" }
    }, grid: { padding: { top: -10, right: -10, left: -10, bottom: -5 } },
    legend: { show: !1 }, plotOptions: {
        pie: {
            donut: {
                size: "75%", labels: {
                    show: !0, value: {
                        fontSize: "1.15rem",
                        fontFamily: "Inter", color: e, fontWeight: 500, offsetY: -18, formatter: function (o) { return parseInt(o) + "%" }
                    },
                    name: { offsetY: 18, fontFamily: "Inter" }, total: {
                        label: "", label: "1 Quarter", show: !0, fontSize: "0.75rem",
                        color: a, fontWeight: 400, formatter: function (o) { return "28%" }
                    }
                }
            }
        }
    }, states: {
        hover: {
            filter: {
                type: "none"
            }
        }, active: { filter: { type: "none" } }
    }
}, n = (null !== n && new ApexCharts(n, l).render(),
    document.querySelector("#totalRevenueChart")), l = {
        chart: {
            height: 80, type: "line", parentHeightOffset: 0, toolbar:
                { show: !1 }, dropShadow: { enabled: !0, color: [config.colors.primary], top: 12, left: 0, blur: 3, opacity: .1 }
        },
        grid: {
            show: !1, xaxis: { lines: { show: !1 } }, yaxis: { lines: { show: !1 } }, padding: {
                top: -15, left: -7, right:
                    9, bottom: -15
            }
        }, colors: [config.colors.primary], stroke: { width: 5, curve: "smooth" }, series: [{
            data: [13, 30,
                20, 35]
        }], tooltip: { shared: !1, intersect: !0, x: { show: !1 } }, tooltip: { enabled: !1 }, xaxis: {
            labels: {
                show:
                    !1
            }, axisTicks: { show: !1 }, axisBorder: { show: !1 }
        }, yaxis: { labels: { show: !1 } }, markers: {
            size: 7,
            strokeColors: "transparent", strokeWidth: 5, offsetX: -3, colors: ["transparent"], discrete: [{
                seriesIndex: 0,
                dataPointIndex: 3, fillColor: o, strokeColor: config.colors.primary, size: 7, shape: "circle"
            }], hover: { size: 7 }
        },
        responsive: [{ breakpoint: 1354, options: { chart: { height: 80 }, markers: { strokeWidth: 4 } } }, {
            breakpoint: 1200,
            options: { chart: { height: 100 } }
        }, { breakpoint: 840, options: { chart: { height: 80 } } }, {
            breakpoint: 768,
            options: { chart: { height: 110 } }
        }]
    }, n = (null !== n && new ApexCharts(n, l).render(),
        document.querySelector("#totalSalesSemiDonutChart")), l = {
            chart: {
                height: 140, type: "radialBar", sparkline: {
                    enabled: !0
                }
            }, plotOptions: {
                radialBar: {
                    hollow: { size: "65%" }, startAngle: -90, endAngle: 90, track: {
                        background: i
                    }, dataLabels: {
                        name: { show: !1 }, value: {
                            offsetY: -2, fontSize: "1.25rem", fontWeight: 500,
                            fontFamily: "Inter", color: a
                        }
                    }
                }
            }, states: {
                hover: { filter: { type: "none" } }, active: {
                    filter: { type: "none" }
                }
            }, stroke: { lineCap: "round" }, colors: [config.colors.info], series: [78], labels: ["Progress"], responsive: [{
                breakpoint: 1600, options: { chart: { height: 160 } }
            }, { breakpoint: 1500, options: { chart: { height: 120 } } }, {
                breakpoint: 1200, options: { chart: { height: 180 } }
            }, { breakpoint: 840, options: { chart: { height: 140 } } }, {
                breakpoint: 768, options: { chart: { height: 180 } }
            }]
        }, n = (null !== n && new ApexCharts(n, l).render(),
            document.querySelector("#newVisitorsChart")), l = {
                chart: {
                    height: 164, type: "bar", parentHeightOffset: 0, toolbar: {
                        show: !1
                    }
                }, plotOptions: {
                    bar: {
                        borderRadius: 5, distributed: !0, columnWidth: "60%", endingShape: "rounded",
                        startingShape: "rounded"
                    }
                }, series: [{ data: [38, 55, 48, 65, 80, 38, 48] }], tooltip: { enabled: !1 }, legend: {
                    show: !1
                }, dataLabels: { enabled: !1 }, colors: [s, s, s, s, config.colors.primary, s, s], grid: {
                    show: !1, padding: {
                        left: -10, top: -10
                    }
                }, states: { hover: { filter: { type: "none" } }, active: { filter: { type: "none" } } }, xaxis: {
                    show: !1, axisTicks: { show: !1 }, axisBorder: { show: !1 }, labels: { show: !1 }
                }, yaxis: { show: !1 }, responsive: [{
                    breakpoint: 1375, options: { chart: { height: 130 } }
                }, { breakpoint: 768, options: { chart: { height: 150 } } }]
            }, n
    = (null !== n && new ApexCharts(n, l).render(), document.querySelector("#webVisitors")), l = {
        chart: {
            width: 160,
            parentHeightOffset: 0, type: "bar", toolbar: { show: !1 }
        }, plotOptions: {
            bar: {
                barHeight: "85%", columnWidth:
                    "35px", startingShape: "rounded", endingShape: "rounded", borderRadius: 3, distributed: !0
            }
        }, colors:
            [config.colors.primary], grid: { padding: { top: -40, left: -12 }, yaxis: { lines: { show: !1 } } }, dataLabels: {
                enabled: !1
            }, series: [{ data: [50, 40, 130, 100, 75, 100, 45, 35] }], tooltip: { enabled: !1 }, legend: { show: !1 },
        xaxis: { labels: { show: !1 }, axisTicks: { show: !1 }, axisBorder: { show: !1 } }, yaxis: { labels: { show: !1 } },
        responsive: [{ breakpoint: 1300, options: { chart: { width: 100 } } }, {
            breakpoint: 1200, options: {
                chart: {
                    width:
                        150
                }
            }
        }]
    }; null !== n && new ApexCharts(n, l).render() }();