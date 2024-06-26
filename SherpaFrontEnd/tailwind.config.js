/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./**/*.{html,razor}"],
    important: true,
    theme: {
        extend: {
            colors: {
                "primary-dark-principal": "var(--primary---dark---principal)",
                "primary-dark-800": "var(--primary---dark--800)",
                "primary-dark-400": "var(--primary---dark--400)",
                "primary-dark-300": "var(--primary---dark--300)",
                "primary-dark-200": "var(--primary---dark--200)",
                "primary-dark-100": "var(--primary---dark--100)",
                "primary-red-900": "var(--primary---red--900)",
                "primary-red-principal-800": "var(--primary---red---principal)",
                "primary-red-400": "var(--primary---red--400)",
                "primary-red-300": "var(--primary---red--300)",
                "primary-red-200": "var(--primary---red--200)",
                "primary-red-100": "var(--primary---red--100)",
                "gray-900": "var(--gray--900)",
                "gray-800": "var(--gray--800)",
                "gray-600": "var(--gray--600)",
                "gray-400": "var(--gray--400)",
                "gray-300": "var(--gray--300)",
                "gray-200": "var(--gray--200)",
                "gray-100": "var(--gray--100)",
                "gray-white": "var(--gray---white)",
                "gray-inner-shadow": "var(--gray---inner--shadow)",
                "states-success-800": "var(--states---success--800)",
                "states-success-400": "var(--states---success--400)",
                "states-success-200": "var(--states---success--200)",
                "states-success-100": "var(--states---success--100)",
                "states-informative-800": "var(--states---informative--800)",
                "states-informative-400": "var(--states---informative--400)",
                "states-informative-200": "var(--states---informative--200)",
                "states-informative-100": "var(--states---informative--100)",
                "states-alert-800": "var(--states---alert--800)",
                "states-alert-400": "var(--states---alert--400)",
                "states-alert-200": "var(--states---alert--200)",
                "states-alert-100": "var(--states---alert--100)",
                "states-error-800": "var(--states---error--800)",
                "states-error-400": "var(--states---error--400)",
                "states-error-200": "var(--states---error--200)",
                "states-error-100": "var(--states---error--100)",
                "secondary-violet-800": "var(--secondary---violet--800)",
                "secondary-violet-300": "var(--secondary---violet--300)",
                "secondary-violet-200": "var(--secondary---violet--200)",
                "secondary-violet-100": "var(--secondary---violet--100)",
                "secondary-orange-800": "var(--secondary---orange--800)",
                "secondary-orange-300": "var(--secondary---orange--300)",
                "secondary-orange-200": "var(--secondary---orange--200)",
                "secondary-orange-100": "var(--secondary---orange--100)",
            },
            fontSize: {
                "title-h1": "var(--title---h1)",
                "title-h2--semibold": "var(--title---h2--semibold)",
                "title-h2--regular": "var(--title---h2--regular)",
                "title-h3": "var(--title---h3)",
                "title-h4": "var(--title---h4)",
                "title-caption-12px": "var(--title---caption-12px)",
                "text-body-16pt-regular": "var(--text---body-16pt--regular)",
                "text-body-16pt-semibold": "var(--text---body-16pt--semibold)",
                "text-body-14pt-semibold": "var(--text---body-14pt--semibold)",
                "text-body-14pt-regular": "var(--text---body-14pt--regular)",
                "text-label": "var(--text---label)",
                "text-button-40px": "var(--text---button-40px)",
                "text-button-32px": "var(--text---button-32px)",
                "text-body-small-12px": "var(--text---body-small-12px)",
                "text-body-small": "var(--text---body-small)",
            }
        },
    },
    plugins: [],
}

