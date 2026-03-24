const apiUri = '/api/Personagens';

// --- LISTAR ---
async function carregarPersonagens() {
    try {
        const resposta = await fetch(apiUri);
        const personagens = await resposta.json();
        document.getElementById('contadorGuerreiros').innerText = personagens.length;
        const corpoTabela = document.getElementById('tabelaPersonagens');
        
        corpoTabela.innerHTML = ""; 

        personagens.forEach(p => {
            corpoTabela.innerHTML += `
                <tr>
                    <td>${p.id}</td>
                    <td>${p.nome}</td>
                    <td>${p.tipo}</td>
                    <td class="text-center">
                        <button class="btn btn-danger btn-sm" onclick="excluirPersonagem(${p.id})">
                            <i class="bi bi-trash"></i> Excluir
                        </button>
                    </td>
                </tr>`;
        });
    } catch (erro) {
        console.error("Erro ao carregar:", erro);
    }
}

// --- CADASTRAR ---
document.getElementById('formPersonagem').addEventListener('submit', async (e) => {
    e.preventDefault(); 

    // Capturando e limpando espaços em branco extras
    const nomeInput = document.getElementById('nome').value.trim();
    const tipoInput = document.getElementById('tipo').value.trim();
   
    // Validação: Se um dos campos estiver vazio, o código para aqui
    if (nomeInput === "" || tipoInput === "") {
        alert("Atenção, Guerreiro! Nome e Tipo são obrigatórios.");
        return; // O 'return' impede que o fetch seja executado
    }
    const novoGuerreiro = {
        nome:nomeInput,
        tipo:tipoInput
    };

    try {
        const resposta = await fetch(apiUri, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(novoGuerreiro)
        });

        if (resposta.ok) {
            document.getElementById('formPersonagem').reset();
            carregarPersonagens(); 
        }
    } catch (erro) {
        console.error("Erro ao cadastrar:", erro);
    }
});

// --- EXCLUIR ---
async function excluirPersonagem(id) {
    if (confirm("Deseja apagar este guerreiro?")) {
        try {
            const resposta = await fetch(`${apiUri}/${id}`, { method: 'DELETE' });
            if (resposta.ok) carregarPersonagens();
        } catch (erro) {
            console.error("Erro ao excluir:", erro);
        }
    }
}

// Inicia a lista
carregarPersonagens();