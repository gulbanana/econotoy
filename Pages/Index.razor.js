let element = null;
let component = null;

function notify() {
    let bounds = element.getBoundingClientRect();
    component.invokeMethodAsync("SetDimensions", bounds.left, bounds.top, element.clientWidth, element.clientHeight);
}

export function subscribe(measureElement, notifyObject) {
    element = measureElement;
    component = notifyObject;
    notify();

    window.addEventListener("resize", notify);
}

export function unsubscribe() {
    if (element != null) {
        window.removeEventListener("onresize", notify);
        element = null;
        component = null;
    }
}