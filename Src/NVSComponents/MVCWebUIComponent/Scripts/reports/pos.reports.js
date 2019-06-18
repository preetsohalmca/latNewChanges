var reportViewer = null;

$(function () {
    "use strict";

    reportViewer = function () {

        function repositionReport() {
            /// <summary>
            ///     Method to reposition report, as this is absolute positioned (as per Telerik site) we need to use javascript to correct position it and in a dynamic way
            /// </summary>
            var parentDoc = $(window.parent.document.body);
            var parentWindow = $(window.parent.window);
            var windowHeight = parentWindow.height();
            var headerHeight = parentDoc.find('#nvsHeader').height();
            var footerHeight = parentDoc.find('#nvsFooter').height();
            var paddings = parseInt(parentDoc.find('#nvsBody').css('padding-top').replace('px', ''));
            paddings += parseInt(parentDoc.find('#nvsFooter').css('padding-top').replace('px', ''));
            paddings += parseInt(parentDoc.find('#nvsBody').css('padding-bottom').replace('px', ''));
            paddings += parseInt(parentDoc.find('#nvsFooter').css('padding-bottom').replace('px', ''));
            parentDoc.find('#reportViewerFrame').height(windowHeight - (headerHeight + footerHeight) - paddings);
        }

        return {

            init: function () {
                /// <summary>
                ///     Initiate the default configuration of telerik reports
                /// </summary>

                repositionReport();

                $(window.parent.window).resize(function () {
                    repositionReport();
                });
            },
            localizeClientElements: function () {
                /// <summary>
                ///     Localize reports' strings
                /// </summary>
                $("button[class*='preview-button']").text($.NVS.localization.Reports_Preview);
                $("a[data-command='telerik_ReportViewer_historyBack']").attr('title', $.NVS.localization.Reports_HistoryBack);
                $("a[data-command='telerik_ReportViewer_historyBack'] span").text($.NVS.localization.Reports_HistoryBack);
                $("a[data-command='telerik_ReportViewer_historyForward']").attr('title', $.NVS.localization.Reports_HistoryForward);
                $("a[data-command='telerik_ReportViewer_historyForward'] span").text($.NVS.localization.Reports_HistoryForward);
                $("a[data-command='telerik_ReportViewer_refresh']").attr('title', $.NVS.localization.Reports_Refresh);
                $("a[data-command='telerik_ReportViewer_refresh'] span").text($.NVS.localization.Reports_Refresh);
                $("a[data-command='telerik_ReportViewer_print']").attr('title', $.NVS.localization.Reports_Print);
                $("a[data-command='telerik_ReportViewer_print'] span").text($.NVS.localization.Reports_Print);
                $("a[data-command='telerik_ReportViewer_export']").attr('title', $.NVS.localization.Reports_Export);
                $("a[data-command='telerik_ReportViewer_export'] span").text($.NVS.localization.Reports_Export);
                $("a[data-command='telerik_ReportViewer_togglePrintPreview']").attr('title', $.NVS.localization.Reports_TogglePrintPreview);
                $("a[data-command='telerik_ReportViewer_togglePrintPreview'] span").text($.NVS.localization.Reports_TogglePrintPreview);
                $("a[data-command='telerik_ReportViewer_goToFirstPage']").attr('title', $.NVS.localization.Reports_GoToFirstPage);
                $("a[data-command='telerik_ReportViewer_goToPrevPage']").attr('title', $.NVS.localization.Reports_GoToPrevPage);
                $("a[data-command='telerik_ReportViewer_goToNextPage']").attr('title', $.NVS.localization.Reports_GoToNextPage);
                $("a[data-command='telerik_ReportViewer_goToLastPage']").attr('title', $.NVS.localization.Reports_GoToLastPage);
                $("a[data-command='telerik_ReportViewer_toggleDocumentMap']").attr('title', $.NVS.localization.Reports_ToggleDocumentMap);
                $("a[data-command='telerik_ReportViewer_toggleParametersArea']").attr('title', $.NVS.localization.Reports_ToggleParametersArea);
                $("a[data-command='telerik_ReportViewer_zoomIn']").attr('title', $.NVS.localization.Reports_ZoomIn);
                $("a[data-command='telerik_ReportViewer_zoomOut']").attr('title', $.NVS.localization.Reports_ZoomOut);
                $("a[data-command='telerik_ReportViewer_toggleZoomMode']").attr('title', $.NVS.localization.Reports_ToggleZoomMode);
                $("a[data-command='telerik_ReportViewer_toggleSideMenu']").attr('title', $.NVS.localization.Reports_ToggleSideMenu);
            }
        };
    }();
});