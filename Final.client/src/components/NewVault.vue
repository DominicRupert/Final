<template>
   <form class="row g-3">
  <div class="col-md-6">
    <label for="inputEmail4" class="form-label">Email</label>
    <input type="email" class="form-control" id="inputEmail4">
  </div>
      <div class="form-check">
      <input class="form-check-input" type="checkbox" id="gridCheck">
      <label class="form-check-label" for="gridCheck">
        Check me out
      </label>
    </div>
  <div class="col-12">
    <button type="submit" class="btn btn-primary">Sign in</button>
  </div>
</form>
</template>


<script>
import { computed, ref } from 'vue'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService.js'
import { useRoute } from 'vue-router'
import { vaultKeepsService } from '../services/VaultKeepsService.js'
import { accountService } from '../services/AccountService.js'
import { logger } from '../utils/Logger.js'
import { Modal } from 'bootstrap'
import  Pop  from '../utils/Pop.js'



export default {
    props: {
        vault: {
            type: Object,
            required: true
        }
    },
    setup(){
        const vault = ref({})
        const route = useRoute();
        return {
            account: computed(() => AppState.account),
            vault: computed(() => AppState.vaults),
            keep: computed(() => AppState.activeKeep),


            
            async createVaults(){
                try {
                    await vaultsService.createVaults(vault.value);
                }
                catch (error) {
                    logger.error(error);
                }
            }
        }
    }
}
</script>


<style lang="scss" scoped>

</style>