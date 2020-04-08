const links = async () => {
    const response = await fetch('oauth/links');
    const data = await response.json()
    return data
}

let auth = {
    linkedin: ''
}
links().then(d => auth = d)

const linkedinAuth = () => {
    window.location.href = auth.linkedin;
}