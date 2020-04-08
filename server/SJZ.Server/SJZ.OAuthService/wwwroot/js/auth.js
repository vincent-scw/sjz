const links = async () => {
    const response = await fetch('oauth/api-links');
    const data = await response.json()
    return data
}

let auth = {
    linkedIn: ''
}
links().then(d => auth = d)

const linkedinAuth = () => {
    window.location.href = auth.linkedIn;
}