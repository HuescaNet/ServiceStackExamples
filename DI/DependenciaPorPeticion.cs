namespace DI
{
    class DependenciaPorPeticion : IDependenciaPorPeticion
    {
        int numeroDeVecesInstanciado;

        public string Mensaje()
        {
            return $"Dependencia por petición {++numeroDeVecesInstanciado} instanciada";
        }
    }
}