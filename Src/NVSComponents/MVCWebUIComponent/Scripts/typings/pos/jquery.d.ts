interface JQueryStatic {
    grep<T>(array: kendo.data.ObservableArray, func: (elementOfArray: T, indexInArray: number) => boolean, invert?: boolean): T[];
}