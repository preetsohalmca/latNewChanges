interface NVSLocalization {
}

interface NVSRequestResult {
    Success: boolean;
    InfoMessages: string[];
    ErrorMessages: string[];
    WarningMessages: string[];
    Content: any;
}

interface NVSJQueryStatic {
    init(): void;
    displayFlashMessages(requestResult: NVSRequestResult, flashMessenger: kendo.ui.Notification): void;
    getWeekNumber(date: Date): number;
    getYearForWeek(date: Date): number;
    getDataType(value: any): string;
    getTypedValue(element: JQuery): any;
    go(url: string): void;
    getValue(element: JQuery): any;
    localization: NVSLocalization;
    staticflashMessager: kendo.ui.Notification;
    selectedFlashMessenger: kendo.ui.Notification;
    summaryFlashMessenger: kendo.ui.Notification;
    popupFlashMessenger: kendo.ui.Notification;
    isClickEventAttached(element: JQuery): boolean;
    isBlank(obj: any): boolean;
    setupCollapsibleElements(): void;
}

interface JQueryStatic {
    NVS: NVSJQueryStatic;
}
