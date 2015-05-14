var SS = SS || {};  // namespace

SS.sample = (function() {
    'use strict';

    function add(a, b) {
        return Math.round(a + b);
    }

    function subtract(a, b) {
        return Math.round(a - b);
    }

    function multiply(a, b) {
        return Math.round(a * b);
    }

    return {
        add: add,
        subtract: subtract,
        multiply: multiply
    };
})();