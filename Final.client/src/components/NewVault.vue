<template>
  <form class="row g-3" @submit="createVaults()">
    <div class="col-12">
      <div class="col-md-6">
        <label for="vault-name" class="form-label">New Vault Name</label>
        <input
          v-model="editable.name"
          type="text"
          class="form-control"
          id="vault-name"
        />
      </div>
      <div class="col-md-6">
        <label for="vault-name" class="form-label">Vault description</label>
        <input
          v-model="editable.description"
          type="text"
          class="form-control"
          id="vault-name"
        />
      </div>
    </div>
    <div class="form-check">
      <input
        v-model="editable.isPrivate"
        class="form-check-input"
        type="checkbox"
        id="gridCheck"
      />
      <label class="form-check-label" for="gridCheck">
        Make this vault private?
      </label>
    </div>
    <div class="col-12">
      <button type="submit" class="btn btn-primary">New Vault</button>
    </div>
  </form>
</template>


<script>
import { computed, ref, watchEffect } from 'vue'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService.js'
import { useRoute } from 'vue-router'
import { useRouter } from 'vue-router'
import { vaultKeepsService } from '../services/VaultKeepsService.js'
import { accountService } from '../services/AccountService.js'
import { logger } from '../utils/Logger.js'
import { Modal } from 'bootstrap'
import Pop from '../utils/Pop.js'



export default {
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const route = useRoute();
    const router = useRouter();
    const editable = ref({})
    watchEffect(() => {
      editable.value = { ...props.vault }
    })
    return {
      editable,
      account: computed(() => AppState.account),
      vault: computed(() => AppState.vaults),
      keep: computed(() => AppState.activeKeep),



      async createVaults() {
        try {
          const newVault = await vaultsService.createVaults(editable.value)
          router.push({
            name: "Vault",
            params: {
              id: newVault.id
            }

          })
          Pop.toast("Vault created");
        } catch (error) {
          logger.error(error);
          Pop.toast(error.message);
        }
      },
    }
  }
}
</script>


<style lang="scss" scoped>
</style>