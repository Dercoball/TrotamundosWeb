﻿/*
 Highcharts JS v7.1.2 (2019-06-03)

 (c) 2009-2019 Torstein Honsi

 License: www.highcharts.com/license
*/
(function (d) { "object" === typeof module && module.exports ? (d["default"] = d, module.exports = d) : "function" === typeof define && define.amd ? define("highcharts/modules/heatmap", ["highcharts"], function (l) { d(l); d.Highcharts = l; return d }) : d("undefined" !== typeof Highcharts ? Highcharts : void 0) })(function (d) {
    function l(b, d, h, u) { b.hasOwnProperty(d) || (b[d] = u.apply(null, h)) } d = d ? d._modules : {}; l(d, "parts-map/ColorAxis.js", [d["parts/Globals.js"]], function (b) {
        var d = b.addEvent, h = b.Axis, u = b.Chart, p = b.color, m, q = b.extend, n = b.isNumber,
        e = b.Legend, k = b.LegendSymbolMixin, v = b.noop, l = b.merge, r = b.pick; m = b.ColorAxis = function () { this.init.apply(this, arguments) }; q(m.prototype, h.prototype); q(m.prototype, {
            defaultColorAxisOptions: { lineWidth: 0, minPadding: 0, maxPadding: 0, gridLineWidth: 1, tickPixelInterval: 72, startOnTick: !0, endOnTick: !0, offset: 0, marker: { animation: { duration: 50 }, width: .01, color: "#999999" }, labels: { overflow: "justify", rotation: 0 }, minColor: "#e6ebf5", maxColor: "#003399", tickLength: 5, showInLegend: !0 }, keepProps: ["legendGroup", "legendItemHeight",
            "legendItemWidth", "legendItem", "legendSymbol"].concat(h.prototype.keepProps), init: function (a, c) { var f = "vertical" !== a.options.legend.layout, g; this.coll = "colorAxis"; g = this.buildOptions.call(a, this.defaultColorAxisOptions, c); h.prototype.init.call(this, a, g); c.dataClasses && this.initDataClasses(c); this.initStops(); this.horiz = f; this.zoomEnabled = !1; this.defaultLegendLength = 200 }, initDataClasses: function (a) {
                var c = this.chart, f, g = 0, b = c.options.chart.colorCount, e = this.options, k = a.dataClasses.length; this.dataClasses =
                f = []; this.legendItems = []; a.dataClasses.forEach(function (a, t) { a = l(a); f.push(a); if (c.styledMode || !a.color) "category" === e.dataClassColor ? (c.styledMode || (t = c.options.colors, b = t.length, a.color = t[g]), a.colorIndex = g, g++, g === b && (g = 0)) : a.color = p(e.minColor).tweenTo(p(e.maxColor), 2 > k ? .5 : t / (k - 1)) })
            }, hasData: function () { return !(!this.tickPositions || !this.tickPositions.length) }, setTickPositions: function () { if (!this.dataClasses) return h.prototype.setTickPositions.call(this) }, initStops: function () {
                this.stops = this.options.stops ||
                [[0, this.options.minColor], [1, this.options.maxColor]]; this.stops.forEach(function (a) { a.color = p(a[1]) })
            }, buildOptions: function (a, c) { var f = this.options.legend, g = "vertical" !== f.layout; return l(a, { side: g ? 2 : 1, reversed: !g }, c, { opposite: !g, showEmpty: !1, title: null, visible: f.enabled }) }, setOptions: function (a) { h.prototype.setOptions.call(this, a); this.options.crosshair = this.options.marker }, setAxisSize: function () {
                var a = this.legendSymbol, c = this.chart, f = c.options.legend || {}, g, b; a ? (this.left = f = a.attr("x"), this.top =
                g = a.attr("y"), this.width = b = a.attr("width"), this.height = a = a.attr("height"), this.right = c.chartWidth - f - b, this.bottom = c.chartHeight - g - a, this.len = this.horiz ? b : a, this.pos = this.horiz ? f : g) : this.len = (this.horiz ? f.symbolWidth : f.symbolHeight) || this.defaultLegendLength
            }, normalizedValue: function (a) { this.isLog && (a = this.val2lin(a)); return 1 - (this.max - a) / (this.max - this.min || 1) }, toColor: function (a, c) {
                var f = this.stops, g, b, e = this.dataClasses, k, d; if (e) for (d = e.length; d--;) {
                    if (k = e[d], g = k.from, f = k.to, (void 0 === g || a >= g) &&
                    (void 0 === f || a <= f)) { b = k.color; c && (c.dataClass = d, c.colorIndex = k.colorIndex); break }
                } else { a = this.normalizedValue(a); for (d = f.length; d-- && !(a > f[d][0]) ;); g = f[d] || f[d + 1]; f = f[d + 1] || g; a = 1 - (f[0] - a) / (f[0] - g[0] || 1); b = g.color.tweenTo(f.color, a) } return b
            }, getOffset: function () { var a = this.legendGroup, c = this.chart.axisOffset[this.side]; a && (this.axisParent = a, h.prototype.getOffset.call(this), this.added || (this.added = !0, this.labelLeft = 0, this.labelRight = this.width), this.chart.axisOffset[this.side] = c) }, setLegendColor: function () {
                var a,
                c = this.reversed; a = c ? 1 : 0; c = c ? 0 : 1; a = this.horiz ? [a, 0, c, 0] : [0, c, 0, a]; this.legendColor = { linearGradient: { x1: a[0], y1: a[1], x2: a[2], y2: a[3] }, stops: this.stops }
            }, drawLegendSymbol: function (a, c) {
                var f = a.padding, g = a.options, b = this.horiz, e = r(g.symbolWidth, b ? this.defaultLegendLength : 12), k = r(g.symbolHeight, b ? 12 : this.defaultLegendLength), d = r(g.labelPadding, b ? 16 : 30), g = r(g.itemDistance, 10); this.setLegendColor(); c.legendSymbol = this.chart.renderer.rect(0, a.baseline - 11, e, k).attr({ zIndex: 1 }).add(c.legendGroup); this.legendItemWidth =
                e + f + (b ? g : d); this.legendItemHeight = k + f + (b ? d : 0)
            }, setState: function (a) { this.series.forEach(function (c) { c.setState(a) }) }, visible: !0, setVisible: v, getSeriesExtremes: function () { var a = this.series, c = a.length; this.dataMin = Infinity; for (this.dataMax = -Infinity; c--;) a[c].getExtremes(), void 0 !== a[c].valueMin && (this.dataMin = Math.min(this.dataMin, a[c].valueMin), this.dataMax = Math.max(this.dataMax, a[c].valueMax)) }, drawCrosshair: function (a, c) {
                var f = c && c.plotX, b = c && c.plotY, e, k = this.pos, d = this.len; c && (e = this.toPixels(c[c.series.colorKey]),
                e < k ? e = k - 2 : e > k + d && (e = k + d + 2), c.plotX = e, c.plotY = this.len - e, h.prototype.drawCrosshair.call(this, a, c), c.plotX = f, c.plotY = b, this.cross && !this.cross.addedToColorAxis && this.legendGroup && (this.cross.addClass("highcharts-coloraxis-marker").add(this.legendGroup), this.cross.addedToColorAxis = !0, this.chart.styledMode || this.cross.attr({ fill: this.crosshair.color })))
            }, getPlotLinePath: function (a) {
                var c = a.translatedValue; return n(c) ? this.horiz ? ["M", c - 4, this.top - 6, "L", c + 4, this.top - 6, c, this.top, "Z"] : ["M", this.left, c, "L",
                this.left - 6, c + 6, this.left - 6, c - 6, "Z"] : h.prototype.getPlotLinePath.apply(this, arguments)
            }, update: function (a, c) { var f = this.chart, b = f.legend, e = this.buildOptions.call(f, {}, a); this.series.forEach(function (a) { a.isDirtyData = !0 }); a.dataClasses && b.allItems && (b.allItems.forEach(function (a) { a.isDataClass && a.legendGroup && a.legendGroup.destroy() }), f.isDirtyLegend = !0); f.options[this.coll] = l(this.userOptions, e); h.prototype.update.call(this, e, c); this.legendItem && (this.setLegendColor(), b.colorizeItem(this, !0)) }, remove: function () {
                this.legendItem &&
                this.chart.legend.destroyItem(this); h.prototype.remove.call(this)
            }, getDataClassLegendSymbols: function () {
                var a = this, c = this.chart, f = this.legendItems, e = c.options.legend, d = e.valueDecimals, p = e.valueSuffix || "", m; f.length || this.dataClasses.forEach(function (e, g) {
                    var h = !0, l = e.from, n = e.to; m = ""; void 0 === l ? m = "\x3c " : void 0 === n && (m = "\x3e "); void 0 !== l && (m += b.numberFormat(l, d) + p); void 0 !== l && void 0 !== n && (m += " - "); void 0 !== n && (m += b.numberFormat(n, d) + p); f.push(q({
                        chart: c, name: m, options: {}, drawLegendSymbol: k.drawRectangle,
                        visible: !0, setState: v, isDataClass: !0, setVisible: function () { h = this.visible = !h; a.series.forEach(function (a) { a.points.forEach(function (a) { a.dataClass === g && a.setVisible(h) }) }); c.legend.colorizeItem(this, h) }
                    }, e))
                }); return f
            }, name: ""
        });["fill", "stroke"].forEach(function (a) { b.Fx.prototype[a + "Setter"] = function () { this.elem.attr(a, p(this.start).tweenTo(p(this.end), this.pos), null, !0) } }); d(u, "afterGetAxes", function () { var a = this.options.colorAxis; this.colorAxis = []; a && new m(this, a) }); d(e, "afterGetAllItems", function (a) {
            var c =
            [], e = this.chart.colorAxis[0]; e && e.options && e.options.showInLegend && (e.options.dataClasses ? c = e.getDataClassLegendSymbols() : c.push(e), e.series.forEach(function (c) { b.erase(a.allItems, c) })); for (e = c.length; e--;) a.allItems.unshift(c[e])
        }); d(e, "afterColorizeItem", function (a) { a.visible && a.item.legendColor && a.item.legendSymbol.attr({ fill: a.item.legendColor }) }); d(e, "afterUpdate", function (a, c, e) { this.chart.colorAxis[0] && this.chart.colorAxis[0].update({}, e) })
    }); l(d, "parts-map/ColorSeriesMixin.js", [d["parts/Globals.js"]],
    function (b) {
        var d = b.defined, h = b.noop, l = b.seriesTypes; b.colorPointMixin = { dataLabelOnNull: !0, isValid: function () { return null !== this.value && Infinity !== this.value && -Infinity !== this.value }, setVisible: function (b) { var d = this, h = b ? "show" : "hide"; d.visible = !!b;["graphic", "dataLabel"].forEach(function (b) { if (d[b]) d[b][h]() }) }, setState: function (d) { b.Point.prototype.setState.call(this, d); this.graphic && this.graphic.attr({ zIndex: "hover" === d ? 1 : 0 }) } }; b.colorSeriesMixin = {
            pointArrayMap: ["value"], axisTypes: ["xAxis", "yAxis",
            "colorAxis"], optionalAxis: "colorAxis", trackerGroups: ["group", "markerGroup", "dataLabelsGroup"], getSymbol: h, parallelArrays: ["x", "y", "value"], colorKey: "value", pointAttribs: l.column.prototype.pointAttribs, translateColors: function () { var b = this, d = this.options.nullColor, h = this.colorAxis, l = this.colorKey; this.data.forEach(function (e) { var k = e[l]; if (k = e.options.color || (e.isNull ? d : h && void 0 !== k ? h.toColor(k, e) : e.color || b.color)) e.color = k }) }, colorAttribs: function (b) {
                var h = {}; d(b.color) && (h[this.colorProp || "fill"] =
                b.color); return h
            }
        }
    }); l(d, "parts-map/HeatmapSeries.js", [d["parts/Globals.js"]], function (b) {
        var d = b.colorPointMixin, h = b.merge, l = b.noop, p = b.pick, m = b.Series, q = b.seriesType, n = b.seriesTypes; q("heatmap", "scatter", { animation: !1, borderWidth: 0, nullColor: "#f7f7f7", dataLabels: { formatter: function () { return this.point.value }, inside: !0, verticalAlign: "middle", crop: !1, overflow: !1, padding: 0 }, marker: null, pointRange: null, tooltip: { pointFormat: "{point.x}, {point.y}: {point.value}\x3cbr/\x3e" }, states: { hover: { halo: !1, brightness: .2 } } },
        h(b.colorSeriesMixin, {
            pointArrayMap: ["y", "value"], hasPointSpecificOptions: !0, getExtremesFromAll: !0, directTouch: !0, init: function () { var e; n.scatter.prototype.init.apply(this, arguments); e = this.options; e.pointRange = p(e.pointRange, e.colsize || 1); this.yAxis.axisPointRange = e.rowsize || 1 }, translate: function () {
                var e = this.options, b = this.xAxis, d = this.yAxis, h = e.pointPadding || 0, l = function (a, b, e) { return Math.min(Math.max(b, a), e) }, a = this.pointPlacementToXValue(); this.generatePoints(); this.points.forEach(function (c) {
                    var f =
                    (e.colsize || 1) / 2, k = (e.rowsize || 1) / 2, m = l(Math.round(b.len - b.translate(c.x - f, 0, 1, 0, 1, -a)), -b.len, 2 * b.len), f = l(Math.round(b.len - b.translate(c.x + f, 0, 1, 0, 1, -a)), -b.len, 2 * b.len), n = l(Math.round(d.translate(c.y - k, 0, 1, 0, 1)), -d.len, 2 * d.len), k = l(Math.round(d.translate(c.y + k, 0, 1, 0, 1)), -d.len, 2 * d.len), q = p(c.pointPadding, h); c.plotX = c.clientX = (m + f) / 2; c.plotY = (n + k) / 2; c.shapeType = "rect"; c.shapeArgs = { x: Math.min(m, f) + q, y: Math.min(n, k) + q, width: Math.abs(f - m) - 2 * q, height: Math.abs(k - n) - 2 * q }
                }); this.translateColors()
            }, drawPoints: function () {
                var b =
                this.chart.styledMode ? "css" : "attr"; n.column.prototype.drawPoints.call(this); this.points.forEach(function (e) { e.graphic[b](this.colorAttribs(e)) }, this)
            }, hasData: function () { return !!this.processedXData.length }, getValidPoints: function (b, d) { return m.prototype.getValidPoints.call(this, b, d, !0) }, animate: l, getBox: l, drawLegendSymbol: b.LegendSymbolMixin.drawRectangle, alignDataLabel: n.column.prototype.alignDataLabel, getExtremes: function () {
                m.prototype.getExtremes.call(this, this.valueData); this.valueMin = this.dataMin;
                this.valueMax = this.dataMax; m.prototype.getExtremes.call(this)
            }
        }), b.extend({ haloPath: function (b) { if (!b) return []; var d = this.shapeArgs; return ["M", d.x - b, d.y - b, "L", d.x - b, d.y + d.height + b, d.x + d.width + b, d.y + d.height + b, d.x + d.width + b, d.y - b, "Z"] } }, d))
    }); l(d, "masters/modules/heatmap.src.js", [], function () { })
});
//# sourceMappingURL=heatmap.js.map