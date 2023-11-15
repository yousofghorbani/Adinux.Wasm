var setindeterminateCheckboxes = () => {
    console.log("jjj")
    document.querySelectorAll('[indeterminate]')
    .forEach($checkbox => $checkbox.indeterminate = true);
}