/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./**/*.{html,razor}"],
    theme: {
        extend: {
            colors: {
                "primary-dark-principal": "var(--primary---dark---principal)",
                "primary-dark-800": "var(--primary---dark--800)",
                "primary-dark-600": "var(--primary---dark--600)",
                "primary-dark-400": "var(--primary---dark--400)",
                "primary-dark-200": "var(--primary---dark--200)",
                "primary-dark-100": "var(--primary---dark--100)",
                "primary-red-900": "var(--primary---red--900)",
                "primary-red-principal": "var(--primary---red---principal)",
                "primary-red-800": "var(--primary---red--800)",
                "primary-red-400": "var(--primary---red--400)",
                "primary-red-200": "var(--primary---red--200)",
                "primary-red-100": "var(--primary---red--100)",
                "gray-900": "var(--gray--900)",
                "gray-800": "var(--gray--800)",
                "gray-600": "var(--gray--600)",
                "gray-400": "var(--gray--400)",
                "gray-200": "var(--gray--200)",
                "gray-100": "var(--gray--100)",
                "gray-white": "var(--gray---white)",
                "states-success-800": "var(--states---success--800)",
                "states-success-300": "var(--states---success--300)",
                "states-success-200": "var(--states---success--200)",
                "states-informative-800": "var(--states---informative--800)",
                "states-informative-300": "var(--states---informative--300)",
                "states-informative-200": "var(--states---informative--200)",
                "states-alert-800": "var(--states---alert--800)",
                "states-alert-300": "var(--states---alert--300)",
                "states-alert-200": "var(--states---alert--200)",
                "states-error-800": "var(--states---error--800)",
                "states-error-600": "var(--states---error--600)",
                "states-error-300": "var(--states---error--300)",
                "states-error-200": "var(--states---error--200)",
            }
        },
    },
    plugins: [],
}

