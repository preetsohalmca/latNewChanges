; (function (trv, $) {
    "use strict";

    var sr = {
        controllerNotInitialized: 'Controller n&atilde;o est&aacute; inicializado.',
        noReportInstance: 'Nenhuma inst&acirc;ncia de relat&oacute;rio.',
        missingTemplate: 'Template do ReportViewer ausente. Por favor especifique o templateUrl nas op&ccedil;&otilde;es.',
        noReport: 'Nenhum relat&oacute;rio.',
        noReportDocument: 'Nenhum documento de relat&oacute;rio.',
        invalidParameter: 'Por favor informe um valor v&aacute;lido.',
        invalidDateTimeValue: 'Por favor informe uma data v&aacute;lida.',
        parameterIsEmpty: 'O valor do par&acirc;metro n&atilde;o pode ser vazio.',
        cannotValidateType: 'O par&acirc;metro do tipo {type} n&atilde;o pode ser validado.',
        loadingFormats: 'Carregando...',
        loadingReport: 'Carregando relat&oacute;rio...',
        preparingDownload: 'Preparando documento para download. Por favor aguarde...',
        preparingPrint: 'Preparando documento para impress&atilde;o. Por favor aguarde...',
        errorLoadingTemplates: 'Erro carregando o template do report viewer.',
        loadingReportPagesInProgress: '{0} p&aacute;ginas carregadas at&eacute; agora...',
        loadedReportPagesComplete: 'Feito. Total de {0} p&aacute;ginas carregadas.',
        noPageToDisplay: "Nenhuma p&aacute;gina a ser exibida.",
        errorDeletingReportInstance: 'Erro excluindo inst&acirc;ncia de relat&oacute;rio: {0}',
        errorRegisteringViewer : 'Erro registrando o report viewer com o servi&ccedil;o.', 
        noServiceClient : 'Nenhum serviceClient foi especificado para este controller.', 
        errorRegisteringClientInstance : 'Erro registrando a inst&acirc;ncia de cliente', 
        errorCreatingReportInstance : 'Erro criando a inst&acirc;ncia do relat&oacute;rio (Relat&oacute;rio = {0})', 
        errorCreatingReportDocument: 'Erro criando o documento do relat&oacute;rio (Relat&oacute;rio = {0}; Formato = {1})',
        unableToGetReportParameters: "N&atilde;o &eacute; poss&iacute;vel obter os par&acirc;metros do relat&oacute;rio",
    };

    trv.sr = $.extend(trv.sr, sr);

}(window.telerikReportViewer = window.telerikReportViewer || {}, jQuery));