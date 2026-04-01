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
                <tr class="align-middle">
            <td style="width: 120px;">
                <div class="card bg-dark border-warning shadow-sm" style="width: 100px; overflow: hidden;">
                    <img src="${p.fotoUrl}" class="card-img-top" 
                         style="height: 100px; object-fit: cover; object-position: top center;" 
                         alt="${p.nome}"
                         onerror="this.src='images/default.jpg'">
                </div>
            </td>
            
            <td class="fw-bold fs-5">${p.nome}</td>
            <td><span class="badge bg-info text-dark">${p.tipo}</span></td>
            <td class="text-warning fw-bold fs-5">${p.poderBase.toLocaleString()}</td>
            
            <td class="text-center">
                <div class="btn-group">
                    <button class="btn btn-outline-danger btn-sm" onclick="excluirPersonagem(${p.id})">
                        <i class="bi bi-trash"></i>
                    </button>
                    <button class="btn btn-warning btn-sm" onclick="elevarKi(${p.id}, '${p.tipo}')">
                        <i class="bi bi-lightning-fill"></i>
                    </button>
                </div>
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
    const poderInput = document.getElementById('poderBase').value;
    const fotoInput = document.getElementById('fotoUrl').value.trim();
   
    // Validação de Pleno: Verifica todos os campos obrigatórios
    if (!nomeInput || !tipoInput || !poderInput || !fotoInput) {
        alert("Atenção! Todos os campos (Nome, Tipo, Poder e Foto) são obrigatórios.");
        return;
    }
    const novoGuerreiro = {
        nome: nomeInput,
        tipo: tipoInput,
        poderBase: parseFloat(poderInput), // Converte para número
        fotoUrl: fotoInput
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
        } else {
            const erroApi = await resposta.text();
            alert("Erro na API: " + erroApi);
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
// --- ELEVAR KI (A Lógica do Delegate no Front) ---
function elevarKi(id, tipo) {
    // Encontra a célula de poder na linha correta
    const botoes = document.querySelectorAll(`button[onclick*="elevarKi(${id}"]`);
    const linha = botoes[0].closest('tr');
    const celulaPoder = linha.cells[3]; // Coluna do Poder

    // Lógica de cálculo (O "Delegate" do Front-end)
    let multiplicador = (tipo.toLowerCase() === 'saiyajin') ? 50 : 2;
    let poderAtual = parseFloat(celulaPoder.innerText.replace(/\./g, ''));
    let novoPoder = poderAtual * multiplicador;

    // Aplica o efeito visual e o novo valor
    celulaPoder.innerHTML = novoPoder.toLocaleString('pt-BR');
    celulaPoder.classList.add('ki-elevado');

    console.log(`Poder de ${tipo} elevado para ${novoPoder}`);
}

// Inicia a lista
carregarPersonagens();