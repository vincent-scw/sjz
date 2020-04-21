const callAuthProvider = (provider) => {
    const urlParams = new URLSearchParams(window.location.search);
    const returnUrl = urlParams.get('ReturnUrl');
    window.location.href = `External/${provider}?returnUrl=${returnUrl}`;
}