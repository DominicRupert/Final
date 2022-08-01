<template>
<Modal id="vault-modal">
    <template #modal-title></template>
    <template #modal-body>
        <div class="row">
            <form class="row g-3">
  <div class="col-auto">
    <label for="staticEmail2" class="visually-hidden">Email</label>
    <input type="text" readonly class="form-control-plaintext" id="staticEmail2" value="Name your new vault">
  </div>
 
</form>
          
  </div>
         
               
            
    </template>
    <template #modal-footer>
        <button @click="close" class="btn btn-dark">Close</button>
    </template>


 </Modal>
</template>


<script>
export default {
    setup(){
        return {}
    }
}
</script>


<style lang="scss" scoped>

</style>