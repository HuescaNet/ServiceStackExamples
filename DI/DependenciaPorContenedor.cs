namespace DI
{
    class DependenciaPorContenedor : IDependenciaPorContenedor
    {
        int numeroDeVecesInstanciado;

        public string Mensaje()
        {
            return $"Dependencia por contenedor {++numeroDeVecesInstanciado} instanciada";
        }
    }
}