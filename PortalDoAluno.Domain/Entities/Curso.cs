using System.ComponentModel;

namespace PortalDoAluno.Domain.Entities
{
    public enum Curso
    {
        [Description("Ciência da Computação")]
        CienciaDaComputacao = 1,

        [Description("Engenharia de Software")]
        EngenhariaDeSoftware = 2,

        [Description("Sistemas de Informação")]
        SistemasDeInformacao = 3,

        [Description("Análise e Desenvolvimento de Sistemas")]
        AnaliseEDesenvolvimentoDeSistemas = 4,

        [Description("Engenharia da Computação")]
        EngenhariaDaComputacao = 5,

        [Description("Gestão da Tecnologia da Informação")]
        GestaoDaTecnologiaDaInformacao = 6,

        [Description("Redes de Computadores")]
        RedesDeComputadores = 7,

        [Description("Segurança da Informação")]
        SegurancaDaInformacao = 8,

        [Description("Banco de Dados")]
        BancoDeDados = 9,

        [Description("Inteligência Artificial")]
        InteligenciaArtificial = 10,

        [Description("Desenvolvimento Web")]
        DesenvolvimentoWeb = 11,

        [Description("Desenvolvimento Mobile")]
        DesenvolvimentoMobile = 12,

        [Description("Big Data e Análise de Dados")]
        BigDataEAnaliseDeDados = 13,

        [Description("Computação em Nuvem")]
        ComputacaoEmNuvem = 14,

        [Description("Internet das Coisas (IoT)")]
        InternetDasCoisas = 15,

        [Description("Machine Learning e Deep Learning")]
        MachineLearningEDeepLearning = 16,

        [Description("Computação Gráfica e Multimídia")]
        ComputacaoGraficaEMultimidia = 17,

        [Description("Automação e Robótica")]
        AutomacaoERobotica = 18,

        [Description("Realidade Virtual e Realidade Aumentada")]
        RealidadeVirtualERealidadeAumentada = 19,

        [Description("Arquitetura de Software")]
        ArquiteturaDeSoftware = 20,

        [Description("DevOps e Engenharia de Confiabilidade de Sites (SRE)")]
        DevOpsESRE = 21,

        [Description("Cibersegurança e Forense Digital")]
        CibersegurancaEForenseDigital = 22,

        [Description("Governança e Compliance em TI")]
        GovernancaEComplianceEmTI = 23,

        [Description("Blockchain e Criptomoedas")]
        BlockchainECriptomoedas = 24
    }
}
