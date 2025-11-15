# PimVIII MAUI Creator

![.NET MAUI](https://img.shields.io/badge/.NET%20MAUI-9.0-512BD4?logo=dotnet)
![C#](https://img.shields.io/badge/C%23-100%25-239120?logo=csharp)
![License](https://img.shields.io/badge/license-MIT-blue.svg)

Aplicação mobile multiplataforma desenvolvida em .NET MAUI para consumir a API do projeto [PIMVIII-API](https://github.com/Vini-Paixao/PIMVIII-API). Este aplicativo permite que criadores de conteúdo gerenciem playlists, conteúdos e visualizem analytics de forma intuitiva.

## 📱 Sobre o Projeto

O **PimVIII MAUI Creator** é uma aplicação mobile que oferece uma interface moderna e responsiva para criadores de conteúdo gerenciarem suas playlists e conteúdos. Desenvolvido com .NET MAUI, o aplicativo funciona em Android, iOS, macOS e Windows.

### Funcionalidades Principais

- 🎵 **Gerenciamento de Playlists**: Criar, editar e organizar playlists
- 📝 **Gerenciamento de Conteúdo**: Adicionar e gerenciar conteúdos multimídia
- 📊 **Analytics**: Visualizar estatísticas e métricas de desempenho
- 🔐 **Autenticação Segura**: Sistema de login e autenticação JWT
- 📱 **Multiplataforma**: Suporte para Android, iOS, macOS e Windows

## 🏗️ Arquitetura

O projeto segue o padrão **MVVM (Model-View-ViewModel)** com a seguinte estrutura:

```
PimVIII-MAUI/
├── Models/              # Modelos de dados
├── ViewModels/          # ViewModels com lógica de negócio
├── Views/              # Páginas XAML da interface
│   ├── AddConteudoPage.xaml
│   ├── AddPlaylistPage.xaml
│   ├── AnalyticsPage.xaml
│   ├── ManageContentPage.xaml
│   ├── ManagePlaylistsPage.xaml
│   └── PlaylistDetailsPage.xaml
├── Services/           # Serviços de API e lógica
├── Messages/           # Mensagens para comunicação entre componentes
├── Resources/          # Recursos (imagens, fontes, etc)
├── Platforms/          # Código específico de plataforma
└── Properties/         # Configurações do projeto
```

## 🚀 Tecnologias Utilizadas

- **.NET 9.0** - Framework principal
- **.NET MAUI** - Framework de UI multiplataforma
- **CommunityToolkit.Mvvm 8.4.0** - Implementação do padrão MVVM
- **C#** - Linguagem de programação
- **XAML** - Linguagem de marcação para UI

## 📋 Pré-requisitos

Antes de começar, certifique-se de ter instalado:

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (17.8 ou superior) com as cargas de trabalho:
  - Desenvolvimento para dispositivos móveis com .NET
  - Desenvolvimento para desktop com .NET
- Para desenvolvimento Android:
  - Android SDK API 21 ou superior
- Para desenvolvimento iOS/macOS:
  - Xcode 15 ou superior (apenas no macOS)
  - Conta de desenvolvedor Apple

## 🔧 Instalação e Configuração

### 1. Clone o repositório

```bash
git clone https://github.com/Vini-Paixao/PimVIII-MAUI.git
cd PimVIII-MAUI
```

### 2. Restaure as dependências

```bash
dotnet restore
```

### 3. Configure a API

Certifique-se de que a API [PIMVIII-API](https://github.com/Vini-Paixao/PIMVIII-API) está rodando e configure a URL base no projeto.

### 4. Execute o projeto

#### Visual Studio
1. Abra a solução `PimVIII.MauiCreator.sln`
2. Selecione a plataforma desejada (Android, iOS, Windows, macOS)
3. Pressione F5 ou clique em "Executar"

#### CLI
```bash
# Android
dotnet build -t:Run -f net9.0-android

# iOS (apenas no macOS)
dotnet build -t:Run -f net9.0-ios

# Windows
dotnet build -t:Run -f net9.0-windows10.0.19041.0

# macOS
dotnet build -t:Run -f net9.0-maccatalyst
```

## 📱 Plataformas Suportadas

| Plataforma | Versão Mínima | Status |
|------------|---------------|--------|
| Android | 5.0 (API 21) | ✅ Suportado |
| iOS | 15.0 | ✅ Suportado |
| macOS | 15.0 | ✅ Suportado |
| Windows | 10.0.17763.0 | ✅ Suportado |

## 📖 Estrutura de Páginas

### AddConteudoPage
Página para adicionar novos conteúdos ao sistema.

### AddPlaylistPage
Permite a criação de novas playlists.

### AnalyticsPage
Exibe métricas e estatísticas de desempenho.

### ManageContentPage
Gerenciamento completo de conteúdos (CRUD).

### ManagePlaylistsPage
Gerenciamento de todas as playlists.

### PlaylistDetailsPage
Visualização detalhada de uma playlist específica.

## 🔐 Autenticação

O aplicativo utiliza autenticação JWT (JSON Web Token) para comunicação segura com a API. Os tokens são armazenados de forma segura usando o `SecureStorage` do .NET MAUI.

## 🎨 Personalização

### Cores do Tema
As cores principais podem ser personalizadas em `App.xaml`:
- Cor primária: `#512BD4`
- Splash screen: Configurado em `Resources/Splash/`
- Ícone do app: Configurado em `Resources/AppIcon/`

### Fontes Customizadas
Adicione fontes personalizadas em `Resources/Fonts/` e registre-as no `MauiProgram.cs`.

## 🧪 Testes

```bash
# Executar testes (quando implementados)
dotnet test
```

## 📦 Build para Produção

### Android
```bash
dotnet publish -f net9.0-android -c Release
```

### iOS
```bash
dotnet publish -f net9.0-ios -c Release
```

### Windows
```bash
dotnet publish -f net9.0-windows10.0.19041.0 -c Release
```

## 🤝 Contribuindo

Contribuições são bem-vindas! Sinta-se à vontade para:

1. Fazer um Fork do projeto
2. Criar uma branch para sua feature (`git checkout -b feature/MinhaFeature`)
3. Commit suas mudanças (`git commit -m 'Adiciona nova feature'`)
4. Push para a branch (`git push origin feature/MinhaFeature`)
5. Abrir um Pull Request

## 📝 Roadmap

- [ ] Implementar autenticação completa
- [ ] Adicionar modo offline
- [ ] Implementar sincronização automática
- [ ] Adicionar testes unitários
- [ ] Melhorar UI/UX
- [ ] Implementar notificações push
- [ ] Adicionar tema claro/escuro
- [ ] Implementar cache de imagens

## 🐛 Problemas Conhecidos

Consulte a [página de Issues](https://github.com/Vini-Paixao/PimVIII-MAUI/issues) para ver problemas conhecidos e reportar novos.

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE.txt](LICENSE.txt) para mais detalhes.

## 👤 Autor

**Vini Paixão**
- GitHub: [@Vini-Paixao](https://github.com/Vini-Paixao)
- Projeto API: [PIMVIII-API](https://github.com/Vini-Paixao/PIMVIII-API)

## 🔗 Links Úteis

- [Documentação .NET MAUI](https://docs.microsoft.com/dotnet/maui/)
- [CommunityToolkit.Mvvm](https://learn.microsoft.com/dotnet/communitytoolkit/mvvm/)
- [PIMVIII-API](https://github.com/Vini-Paixao/PIMVIII-API)

## 📞 Suporte

Se você tiver alguma dúvida ou problema, abra uma [issue](https://github.com/Vini-Paixao/PimVIII-MAUI/issues) ou entre em contato.

---

⭐ Se este projeto foi útil para você, considere dar uma estrela!
